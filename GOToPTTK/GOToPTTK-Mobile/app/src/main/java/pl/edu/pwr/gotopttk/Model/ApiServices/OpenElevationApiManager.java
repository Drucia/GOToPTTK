package pl.edu.pwr.gotopttk.Model.ApiServices;


import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class OpenElevationApiManager {
    private Retrofit retrofit;
    private final static String API_URL = "https://api.open-elevation.com/api/v1/";
    public final static String LOCATIONS_FORMAT = "%f,%f|%f,%f";

    private void initializeRetrofit()
    {
        if (retrofit == null) {
            retrofit = new Retrofit
                    .Builder()
                    .baseUrl(API_URL)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
    }

    public ElevationService getElevationService(){
        initializeRetrofit();
        return retrofit.create(ElevationService.class);
    }
}
