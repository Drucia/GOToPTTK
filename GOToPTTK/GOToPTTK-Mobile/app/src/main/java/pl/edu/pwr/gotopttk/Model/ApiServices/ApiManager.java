package pl.edu.pwr.gotopttk.Model.ApiServices;


import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ApiManager {
    private Retrofit retrofit;
    private Retrofit ret;
    private final static String API_URL = "http://192.168.8.132:45455/api/";
    private final static String API_URL_TRIP = "http://192.168.0.150:45455/api/";

    public RoutesService getRoutesService(){
        initializeRetrofit();
        return retrofit.create(RoutesService.class);
    }

    public TripRoutesService getTripRoutesService(){
        initialRetrofit();
        return ret.create(TripRoutesService.class);
    }

    public TripListService getTripListService(){
        initialRetrofit();
        return ret.create(TripListService.class);
    }

    public PlacesService getPlacesService(){
        initializeRetrofit();
        return retrofit.create(PlacesService.class);
    }

    public MountainGroupsService getMountainRegionsService(){
        initializeRetrofit();
        return retrofit.create(MountainGroupsService.class);
    }

    public TripService getTripService(){
        initializeRetrofit();
        return retrofit.create(TripService.class);
    }

    public VerifyService getVerifyService(){
        initialRetrofit();
        return ret.create(VerifyService.class);
    }

    public ImagesService getImagesService()
    {
        initialRetrofit();
        return ret.create(ImagesService.class);
    }
    private void initialRetrofit()
    {
        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        final OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .connectTimeout(20, TimeUnit.SECONDS)
                .writeTimeout(20, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .build();

        if (ret == null) {
            ret = new Retrofit
                    .Builder()
                    .client(okHttpClient)
                    .baseUrl(API_URL_TRIP)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
    }

    private void initializeRetrofit()
    {
        Gson gson = new GsonBuilder()
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        if (retrofit == null) {
            retrofit = new Retrofit
                    .Builder()
                    .baseUrl(API_URL)
                    .addConverterFactory(GsonConverterFactory.create(gson))
                    .build();
        }
    }
}
