using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using StoreCredentials.WindowsPhoneSL;
using StoreCredentials.WindowsPhoneSL.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CredentialsService))]

namespace StoreCredentials.WindowsPhoneSL
{
    /// <summary>
    /// http://depblog.weblogs.us/2014/11/20/migrating-from-sl8-0-protectdata-to-rt8-1-passwordvault/
    /// </summary>
    public class CredentialsService : ICredentialsService
    {
        private const string CREDFILE = "StoreCredentials.dat";
        private readonly IsolatedStorageFile _userStoreForAppFile = IsolatedStorageFile.GetUserStoreForApplication();

        public string UserName
        {
            get
            {
                var account = this.RetrieveHashedCredentials();
                return (account != null) ? account.UserName : null;
            }
        }

        public string Password
        {
            get
            {
                var account = this.RetrieveHashedCredentials();
                return (account != null) ? account.Password : null;
            }
        }

        private void SaveToFile(byte[] encryptedPasswordData, string fileName)
        {
            if (_userStoreForAppFile.FileExists(fileName))
                _userStoreForAppFile.DeleteFile(fileName);

            IsolatedStorageFileStream fileAsStream = new IsolatedStorageFileStream(fileName, System.IO.FileMode.Create, FileAccess.Write, _userStoreForAppFile);
            Stream writer = new StreamWriter(fileAsStream).BaseStream;
            writer.Write(encryptedPasswordData, 0, encryptedPasswordData.Length);
            writer.Close();
            fileAsStream.Close();
        }

        private byte[] ReadFromFile(string fileName)
        {
            byte[] data = null;

            if (_userStoreForAppFile.FileExists(fileName))
            {
                IsolatedStorageFileStream fileAsStream = new IsolatedStorageFileStream(fileName, System.IO.FileMode.Open, FileAccess.Read, _userStoreForAppFile);
                Stream reader = new StreamReader(fileAsStream).BaseStream;
                data = new byte[reader.Length];
                reader.Read(data, 0, data.Length);
                reader.Close();
                fileAsStream.Close();
            }

            return data;
        }

        private void StoreHashedCredentials(Account account)
        {
            string accountJson = JsonConvert.SerializeObject(account);

            //Convert Password and Salt values to byte[] arrays
            byte[] accountByte = Encoding.UTF8.GetBytes(accountJson);

            //Encrypt Password and Salt byte[] arrays using Protect() method
            byte[] protectedAccountByte = ProtectedData.Protect(accountByte, null);

            //Save byte[] arrays as two files in Isolated Storage
            SaveToFile(protectedAccountByte, CREDFILE);
        }

        private Account RetrieveHashedCredentials()
        {
            Account account = null;

            //Read byte[] arrays from files
            byte[] protectedAccountByte = ReadFromFile(CREDFILE);

            if (!ReferenceEquals(protectedAccountByte, null))
            {
                //Decrypt Password and Salt byte[] arrays using Unprotect() method
                byte[] accountByte = ProtectedData.Unprotect(protectedAccountByte, null);

                //Convert byte[] arrays to strings and display in the text boxes
                string accountJson = Encoding.UTF8.GetString(accountByte, 0, accountByte.Length);
                if (!string.IsNullOrEmpty(accountJson))
                    account = JsonConvert.DeserializeObject<Account>(accountJson);
            }

            return account;
        }

        public void SaveCredentials(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                Account account = new Account() { UserName = userName, Password = password};
                StoreHashedCredentials(account);
            }
        }

        public void DeleteCredentials()
        {
            if (_userStoreForAppFile.FileExists(CREDFILE))
                _userStoreForAppFile.DeleteFile(CREDFILE);
        }

        public bool DoCredentialsExist()
        {
            return _userStoreForAppFile.FileExists(CREDFILE);
        }
    }
}
