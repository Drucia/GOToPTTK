package pl.edu.pwr.gotopttk.Model.Entities;


import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class PlannedTrip implements Parcelable {

    @SerializedName("date")
    @Expose
    private Date date;

    @SerializedName("routes")
    @Expose
    private List<Route> routes;

    @SerializedName("userId")
    @Expose
    private int userId = 1;

    public PlannedTrip(){
        date = Calendar.getInstance().getTime();
        routes = new ArrayList<>();
    }

    public PlannedTrip(Date date) {
        this.date = date;
        routes = new ArrayList<>();
    }

    public List<Route> getRoutes() {
        return routes;
    }

    public int getPoints(){
        return routes.stream().mapToInt(r -> r.points).sum();
    }

    public void addRoute(Route route){
        routes.add(route);
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public boolean isRouteConnectedToTrip(Route route){
        return getRoutes().stream().anyMatch(route::isRouteConnected) && getRoutes().stream().noneMatch(route::isRouteEquivalent);
    }

    public boolean isEmpty(){
        return getRoutes().isEmpty();
    }

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeLong(this.date != null ? this.date.getTime() : -1);
        dest.writeTypedList(this.routes);
    }

    protected PlannedTrip(Parcel in) {
        long tmpDate = in.readLong();
        this.date = tmpDate == -1 ? null : new Date(tmpDate);
        this.routes = in.createTypedArrayList(Route.CREATOR);
    }

    public static final Parcelable.Creator<PlannedTrip> CREATOR = new Parcelable.Creator<PlannedTrip>() {
        @Override
        public PlannedTrip createFromParcel(Parcel source) {
            return new PlannedTrip(source);
        }

        @Override
        public PlannedTrip[] newArray(int size) {
            return new PlannedTrip[size];
        }
    };
}
