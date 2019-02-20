package pl.edu.pwr.gotopttk.Model.Entities;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.Calendar;
import java.util.Date;

public class VerifyRequest {

    @SerializedName("VerifyDate")
    @Expose
    public Date verifyDate;
    @SerializedName("SendVerifyDate")
    @Expose
    public Date sendVerifyDate;
    @SerializedName("Issues")
    @Expose
    public String issues;
    @SerializedName("GuideId")
    @Expose
    public int guideId;
    @SerializedName("TripId")
    @Expose
    public int tripId;
    @SerializedName("TripRouteId")
    @Expose
    public Integer tripRouteId;
    @SerializedName("VerificationStatusId")
    @Expose
    public String verificationStatusId;

    public VerifyRequest(Date sendDate, String issues, int tripId, int guideId, int trip_route_id, String state){
        verifyDate = Calendar.getInstance().getTime();
        sendVerifyDate = sendDate;
        this.issues = issues;
        this.tripId = tripId;
        this.guideId = guideId;
        this.tripRouteId = trip_route_id;
        this.verificationStatusId = state;
    }
}
