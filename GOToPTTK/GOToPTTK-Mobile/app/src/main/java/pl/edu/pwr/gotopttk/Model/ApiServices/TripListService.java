package pl.edu.pwr.gotopttk.Model.ApiServices;

import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Trip;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface TripListService {
    @GET("guides/{id}/trips")
    Call<List<Trip>> getTripsToVerify(
            @Path("id") int gID);
}
