package pl.edu.pwr.gotopttk.Model.ApiServices;


import pl.edu.pwr.gotopttk.Model.Entities.PlannedTrip;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface TripService {

    @POST("trips")
    Call<PlannedTrip> createNewTrip(@Body PlannedTrip plannedTrip);
}
