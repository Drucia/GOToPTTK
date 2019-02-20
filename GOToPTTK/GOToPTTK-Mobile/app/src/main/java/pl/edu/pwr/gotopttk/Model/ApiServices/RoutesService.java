package pl.edu.pwr.gotopttk.Model.ApiServices;


import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.CustomRouteRequest;
import pl.edu.pwr.gotopttk.Model.Entities.Route;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;


public interface RoutesService {

    @GET("routes")
    Call<List<Route>> getRoutes();

    @POST("routes/custom-route")
    Call<List<Route>> createCustomRoute(@Body CustomRouteRequest request);
}
