package pl.edu.pwr.gotopttk.Model.ApiServices;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface ImagesService {
    @GET("images/{routeid}/{edge}")
    Call<ResponseBody> getImagesOfTripRoute(
            @Path("routeid") int rID,
            @Path("edge") String edge);
}