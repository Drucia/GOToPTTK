package pl.edu.pwr.gotopttk.Model.ApiServices;

import pl.edu.pwr.gotopttk.Model.Entities.VerifyRequest;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface VerifyService {
    @POST("Verifications")
    Call<VerifyRequest> createNewVerify(@Body VerifyRequest verifyRequest);
}
