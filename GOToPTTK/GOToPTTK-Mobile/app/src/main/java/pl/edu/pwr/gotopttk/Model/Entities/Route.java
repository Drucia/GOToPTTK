package pl.edu.pwr.gotopttk.Model.Entities;


import android.os.Parcel;
import android.os.Parcelable;
import android.view.ViewDebug;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import kotlinx.android.parcel.Parcelize;


public class Route implements Parcelable {

    public static int NO_ID = -1;
    @SerializedName("id")
    @Expose
    public int id;
    @SerializedName("points")
    @Expose
    public int points;
    @SerializedName("start")
    @Expose
    public Place start;
    @SerializedName("end")
    @Expose
    public Place end;
    @SerializedName("mountainGroup")
    @ViewDebug.ExportedProperty
    public String mountainGroup;

    @Override
    public String toString() {
        return start.toString() + " - " + end.toString();
    }

    public boolean isRouteConnected(Route route){
        return this.id != route.id && (start.id == route.start.id || end.id == route.end.id) || (start.id == route.end.id || end.id == route.start.id);
    }

    public boolean isRouteEquivalent(Route route){
        return (start.id == route.start.id && end.id == route.end.id) || (start.id == route.end.id && end.id == route.start.id);
    }
    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeInt(this.id);
        dest.writeInt(this.points);
        dest.writeParcelable(this.start, flags);
        dest.writeParcelable(this.end, flags);
        dest.writeString(this.mountainGroup);
    }

    public Route() {
        id = NO_ID;
    }

    protected Route(Parcel in) {
        this.id = in.readInt();
        this.points = in.readInt();
        this.start = in.readParcelable(Place.class.getClassLoader());
        this.end = in.readParcelable(Place.class.getClassLoader());
        this.mountainGroup = in.readString();
    }

    public static final Parcelable.Creator<Route> CREATOR = new Parcelable.Creator<Route>() {
        @Override
        public Route createFromParcel(Parcel source) {
            return new Route(source);
        }

        @Override
        public Route[] newArray(int size) {
            return new Route[size];
        }
    };
}