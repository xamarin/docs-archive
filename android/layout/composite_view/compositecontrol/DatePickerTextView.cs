
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace com.xamarin.recipes.compositecontrol
{
    public class DatePickerTextView : LinearLayout
    {
        public EventHandler<DatePickerChangedArgs> DateChanged = (sender, e) => { };

        static readonly string KEY_PARENT_STATE = "datepicker_parent_state";
        static readonly string KEY_DATEPICKER_STATE = "datepicker_state";
        TextView textView;
        ImageButton dateButton;
        DateTime theDate = DateTime.Now;


        public DatePickerTextView(Context context) : base(context)
        {
            Initialize(Tuple.Create<Context, IAttributeSet, int>(context, null, 0));
        }

        public DatePickerTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(Tuple.Create<Context, IAttributeSet, int>(context, attrs, 0));
        }

        public DatePickerTextView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs)
        {
            Initialize(Tuple.Create<Context, IAttributeSet, int>(context, attrs, defStyle));
        }

        protected override IParcelable OnSaveInstanceState()
        {
            Bundle bundle = new Bundle();
            bundle.PutParcelable(KEY_PARENT_STATE, base.OnSaveInstanceState());
            bundle.PutString(KEY_DATEPICKER_STATE, textView.Text);
            return bundle;
        }

        protected override void OnRestoreInstanceState(IParcelable state)
        {
            var bundle = state as Bundle;
            if (bundle == null)
            {
                base.OnRestoreInstanceState(state);
                textView.Text = bundle.GetString(KEY_DATEPICKER_STATE, "");
            }
            else
            {
                base.OnRestoreInstanceState(state);
            }
        }

        void Initialize(Tuple<Context, IAttributeSet, int> values)
        {
            InitializeLinearLayoutProperties();

            var inflater = LayoutInflater.FromContext(values.Item1);
            inflater.Inflate(Resource.Layout.date_picker_layout, this);
            dateButton = FindViewById<ImageButton>(Resource.Id.pick_date_button);
            textView = FindViewById<TextView>(Resource.Id.date_text_view);

            dateButton.Click += DateButton_Click;

            UpdateDisplayedDate();
        }

        void UpdateDisplayedDate()
        {
            textView.Text = theDate.ToString("yyyy-MMM-dd");
        }

        void InitializeLinearLayoutProperties()
        {
            SetGravity(GravityFlags.CenterVertical);
            Orientation = Orientation.Horizontal;
            int paddingStartEnd = Convert.ToInt32(Resources.GetDimension(Resource.Dimension.datepicker_padding_startend));
            int paddingTopBottom = Convert.ToInt32(Context.Resources.GetDimension(Resource.Dimension.datepicker_padding_topbottom));
            SetPadding(paddingStartEnd, paddingTopBottom, paddingStartEnd, paddingTopBottom);
        }

        void DateButton_Click(object sender, EventArgs e)
        {
            var dialog = MyDatePickerDialog.Create(this);
            dialog.Show(this.HostActivity().FragmentManager, "date_picker_dialog");
        }

        void OnDateSet(object sender, Android.App.DatePickerDialog.DateSetEventArgs e)
        {
            theDate = e.Date;
            UpdateDisplayedDate();
            DateChanged(this, new DatePickerChangedArgs(e.Date));
        }

        public DateTime Date
        {
            get
            {
                return theDate;
            }

            set
            {

                theDate = value;
            }
        }

        class MyDatePickerDialog : DialogFragment
        {
            void HandleEventHandler(object sender, DatePickerDialog.DateSetEventArgs e)
            {
            }

            DatePickerTextView hostWidget = null;
            public static MyDatePickerDialog Create(DatePickerTextView view)
            {
                var frag = new MyDatePickerDialog()
                {
                    hostWidget = view ?? throw new NullReferenceException("Must have a valid DatePickerTextView for the host.")
                };
                return frag;
            }

            public override Android.App.Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                int year = hostWidget.theDate.Year;
                int month = hostWidget.theDate.Month - 1; // DatePickerDialog months go from 0 - 11
                int dayOfMonth = hostWidget.theDate.Day;

                var dialog = new DatePickerDialog(hostWidget.HostActivity(), hostWidget.OnDateSet, year, month, dayOfMonth);
                return dialog;
            }
        }
    }
}