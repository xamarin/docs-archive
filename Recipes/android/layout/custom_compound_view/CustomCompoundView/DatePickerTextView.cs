
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace com.xamarin.recipes.customcompoundview
{
	[Register("com.xamarin.recipes.compositecontrol.DatePickerTextView")]
    public class DatePickerTextView : LinearLayout
    {
        public EventHandler<DatePickerTextViewChangedArgs> DateChanged = (sender, e) => { };

        static readonly string DEFAULT_DATE_FORMAT = "yyyy-MMM-dd";
        static readonly string KEY_PARENT_STATE = "datepicker_parent_state";
        static readonly string KEY_DATEPICKER_STATE = "datepicker_state";
        TextView textView;
        ImageButton dateButton;
        DateTime theDate = DateTime.Now;

        public DatePickerTextView(Context context) : base(context)
        {
            Initialize(Tuple.Create<IAttributeSet, int>(null, 0));
        }

        public DatePickerTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(Tuple.Create(attrs, 0));
        }

        public DatePickerTextView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs)
        {
            Initialize(Tuple.Create(attrs, defStyle));
        }

        protected override IParcelable OnSaveInstanceState()
        {
            Bundle bundle = new Bundle();
            bundle.PutParcelable(KEY_PARENT_STATE, base.OnSaveInstanceState());
            bundle.PutString(KEY_DATEPICKER_STATE, theDate.ToString(DEFAULT_DATE_FORMAT));
            return bundle;
        }

        protected override void OnRestoreInstanceState(IParcelable state)
        {
            var bundle = state as Bundle;
            if (bundle == null)
            {
                // Make sure to call the base class OnRestoreInstanceState
                base.OnRestoreInstanceState(state); 
                string date = bundle.GetString(KEY_DATEPICKER_STATE, "");
                if (string.IsNullOrWhiteSpace(date))
                {
                    theDate = DateTime.Now;
                }
                else
                {
                    SetDateInternal(date);
                }
            }
            else
            {
                base.OnRestoreInstanceState(state);
            }
        }

        void Initialize(Tuple<IAttributeSet, int> values)
        {
            InitializeLinearLayoutProperties();
            InitializeStyleAttributeProperties(values.Item1);
            InflateLayout();
            UpdateDisplayedDate();
        }

        void InflateLayout()
        {
            var inflater = LayoutInflater.FromContext(this.Context);
            inflater.Inflate(Resource.Layout.date_picker_layout, this);

			textView = FindViewById<TextView>(Resource.Id.date_text_view);
		
            dateButton = FindViewById<ImageButton>(Resource.Id.pick_date_button);
			dateButton.Click += DateButton_Click;
		}

        void UpdateDisplayedDate()
        {
            textView.Text = theDate.ToString(DEFAULT_DATE_FORMAT);
        }

        void InitializeLinearLayoutProperties()
        {
            SetGravity(GravityFlags.CenterVertical);
            Orientation = Orientation.Horizontal;
            int paddingStartEnd = Convert.ToInt32(Resources.GetDimension(Resource.Dimension.datepicker_padding_startend));
            int paddingTopBottom = Convert.ToInt32(Context.Resources.GetDimension(Resource.Dimension.datepicker_padding_topbottom));
            SetPadding(paddingStartEnd, paddingTopBottom, paddingStartEnd, paddingTopBottom);
        }

        void InitializeStyleAttributeProperties( IAttributeSet attrs)
        {

            var typedArray = this.Context.ObtainStyledAttributes(attrs, Resource.Styleable.DatePicker);
            InitializeDateFromCustomViewAttributes(typedArray);
        }

        void InitializeDateFromCustomViewAttributes(Android.Content.Res.TypedArray typedArray)
        {
            string newDateValue = typedArray.GetString(Resource.Styleable.DatePicker_date);
            if (!string.IsNullOrWhiteSpace(newDateValue))
            {
                SetDateInternal(newDateValue);
            }
        }

        void SetDateInternal(string newDateValue)
        {
            if (!DateTime.TryParse(newDateValue, out theDate))
            {
                throw new ArgumentException("Could not parse the `date` value from the widget attributes for the DatePickerTextView.");
            }
        }

        void DateButton_Click(object sender, EventArgs e)
        {
            var dialog = DatePickerTextViewCalendarDialogFragment.Create(this);
            dialog.Show(this.HostActivity().FragmentManager, "date_picker_dialog");
        }

        void OnDateSet(object sender, Android.App.DatePickerDialog.DateSetEventArgs e)
        {
            theDate = e.Date;
            UpdateDisplayedDate();
            DateChanged(this, new DatePickerTextViewChangedArgs(e.Date));
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

        /// <summary>
        /// A DialogFragment that will host/display the DatePickerDialog
        /// for the DatePickerTextView.
        /// </summary>
        class DatePickerTextViewCalendarDialogFragment : DialogFragment
        {
            DatePickerTextView hostWidget;

            public static DatePickerTextViewCalendarDialogFragment Create(DatePickerTextView view)
            {
                var frag = new DatePickerTextViewCalendarDialogFragment()
                {
                    hostWidget = view ?? throw new NullReferenceException("Must have a valid DatePickerTextView for the host.")
                };
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
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