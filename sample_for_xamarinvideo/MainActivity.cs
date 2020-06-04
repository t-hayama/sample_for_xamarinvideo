using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
//using android_app_2;

namespace sample_for_xamarinvideo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        VideoView videoView;
        TextView textview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            // VideoViewのオブジェクトを取得して再生を開始
            videoView = FindViewById<VideoView>(Resource.Id.videoview);

            // >> video のファイルを Resource > Drawableフォルダに入れて，fileをファイル名に書き換える<<
            videoView.SetVideoPath($"android.resource://" + PackageName + "/" + Resource.Drawable.file);

            //MediaController mediaController = new MediaController(this);
            //mediaController.SetAnchorView(videoView);

            //videoView.Visibility = ViewStates.Visible;
            //videoView.SetMediaController(mediaController);

            videoView.Touch += VideoView_Touch;
            videoView.Start();

            textview = FindViewById<TextView>(Resource.Id.txtMsg);

            new System.Threading.Thread(
                new System.Threading.ThreadStart(delegate ()
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(100);
                        RunOnUiThread(() => textview.Text = videoView.CurrentPosition.ToString());

                        // ここでビデオの再生時間に応じた処理

                    }
                })
                ).Start();
        }

        private void VideoView_Touch(object sender, View.TouchEventArgs e)
        {
            x_posi = e.Event.GetX();
            y_posi = e.Event.GetY();

            // ビデオにクリックしたときの処理

            textview.SetX(x_posi);
            textview.SetY(y_posi);
            Toast.MakeText(this, "X:" + x_posi + "  Y:" + y_posi + "  " + videoView.CurrentPosition.ToString(), ToastLength.Long).Show();
        }

        float x_posi = 0;
        float y_posi = 0;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}