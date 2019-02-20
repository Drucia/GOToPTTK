package pl.edu.pwr.gotopttk.Presenter.Presenters;

import pl.edu.pwr.gotopttk.Model.ApiServices.ApiManager;
import pl.edu.pwr.gotopttk.Model.Entities.VerifyRequest;
import pl.edu.pwr.gotopttk.View.Interfaces.IVerifyView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class VerifyPresenter {
    private ApiManager apiManager;
    private IVerifyView verifyView;
    public static final int OK_REQUEST = 1;
    public static final int ERROR_REQUEST = 0;

    public VerifyPresenter(IVerifyView verifyView) {
        this.apiManager = new ApiManager();
        this.verifyView = verifyView;
    }

    public void createVerify(VerifyRequest verifyRequest){
        if(verifyRequest != null){
            apiManager.getVerifyService().createNewVerify(verifyRequest).enqueue(new Callback<VerifyRequest>() {
                @Override
                public void onResponse(Call<VerifyRequest> call, Response<VerifyRequest> response) {
                    VerifyRequest verify = response.body();
                    if(verify != null){
                        verifyView.displayToast(OK_REQUEST);
                    }
                    else{
                        verifyView.displayToast(ERROR_REQUEST);
                    }
                }

                @Override
                public void onFailure(Call<VerifyRequest> call, Throwable t) {
                     verifyView.displayToast(ERROR_REQUEST);
                }
            });
        }
    }
}
