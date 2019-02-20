package pl.edu.pwr.gotopttk.Model.ApiServices;


import pl.edu.pwr.gotopttk.Model.Entities.OpenElevationResult;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface ElevationService {

    @GET("lookup")
    Call<OpenElevationResult> getElevationResult(@Query("locations") String locations);
}
