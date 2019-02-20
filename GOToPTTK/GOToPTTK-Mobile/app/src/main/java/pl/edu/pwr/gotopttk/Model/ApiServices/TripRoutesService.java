package pl.edu.pwr.gotopttk.Model.ApiServices;

import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.TripRoute;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface TripRoutesService {
    @GET("guides/{id}/trips/{tId}")
    Call<List<TripRoute>> getTripRoutesToVerify(
            @Path("id") int id,
            @Path("tId") int tId);
}
