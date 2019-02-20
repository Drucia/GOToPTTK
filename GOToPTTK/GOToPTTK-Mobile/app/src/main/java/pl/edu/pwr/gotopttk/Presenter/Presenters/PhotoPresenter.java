package pl.edu.pwr.gotopttk.Presenter.Presenters;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.annotation.NonNull;

import okhttp3.ResponseBody;
import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.View.Interfaces.IVerifyWithout;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PhotoPresenter {
    private ApiManager apiManager;
    private IVerifyWithout verifyView;

    public PhotoPresenter(IVerifyWithout view)
    {
        apiManager = new ApiManager();
        verifyView = view;
    }

    public void getImagesToView(int routeId, String edge)
    {
        apiManager.getImagesService()
                .getImagesOfTripRoute(routeId, edge)
                .enqueue(new Callback<ResponseBody>() {
                    @Override
                    public void onResponse(@NonNull Call<ResponseBody> call, @NonNull Response<ResponseBody> response) {
                        if (response.body() != null) {
                            // display the image data in a ImageView or save it
                            Bitmap bmp = BitmapFactory.decodeStream(response.body().byteStream());
                            verifyView.putImgesToView(bmp, edge);
                        }
                        else
                            verifyView.putImgesToView(null, "");
                    }
                    @Override
                    public void onFailure(@NonNull Call<ResponseBody> call, @NonNull Throwable t) {
                        verifyView.displayCommunicate("Ooops... Błąd połączenia z serwerem. Spróbuj ponownie za chwilę :)");
                    }
                });
    }
}
