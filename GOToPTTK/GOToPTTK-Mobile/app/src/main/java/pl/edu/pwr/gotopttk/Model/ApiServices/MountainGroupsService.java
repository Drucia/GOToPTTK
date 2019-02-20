package pl.edu.pwr.gotopttk.Model.ApiServices;


import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface MountainGroupsService {

    @GET("mountainGroups")
    Call<List<String>> getMountainGroupsNames();
}
