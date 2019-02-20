package pl.edu.pwr.gotopttk.Model.Entities;


import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class CustomRouteRequest {

    @SerializedName("start")
    @Expose
    private Place start;

    @SerializedName("end")
    @Expose
    private Place end;

    public CustomRouteRequest(Place start, Place end) {
        this.start = start;
        this.end = end;
    }

    public Place getStart() {
        return start;
    }

    public void setStart(Place start) {
        this.start = start;
    }

    public Place getEnd() {
        return end;
    }

    public void setEnd(Place end) {
        this.end = end;
    }
}
