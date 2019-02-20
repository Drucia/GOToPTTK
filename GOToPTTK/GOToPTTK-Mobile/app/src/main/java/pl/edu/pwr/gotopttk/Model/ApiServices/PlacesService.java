package pl.edu.pwr.gotopttk.Model.ApiServices;


import java.util.List;

import pl.edu.pwr.gotopttk.Model.Entities.Place;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface PlacesService {

    @GET("places")
    Call<List<Place>> getPlaces(@Query("partOfRoute") boolean partOfRoute);

    @GET("places")
    Call<List<Place>> getPlaces();
}
