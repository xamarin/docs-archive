using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Views;

namespace DrawingRecipe
{
    public class MyOvalShape : View
    {
        private readonly ShapeDrawable _shape;

        public MyOvalShape(Context context) : base(context)
        {
            var paint = new Paint();
            paint.SetARGB(255, 200, 255, 0);
            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = 4;

            _shape = new ShapeDrawable(new OvalShape());
            _shape.Paint.Set(paint);

            _shape.SetBounds(20, 20, 300, 200);
        }

        protected override void OnDraw(Canvas canvas)
        {
            _shape.Draw(canvas);
        }
    }
}