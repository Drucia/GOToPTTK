package pl.edu.pwr.gotopttk.Model.Entities;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class Trip implements Parcelable{

    @SerializedName("id")
    @Expose
    public int id;
    @SerializedName("points")
    @Expose
    public int points;
    @SerializedName("startDate")
    @Expose
    public String startDate;
    @SerializedName("endDate")
    @Expose
    public String endDate;
    @SerializedName("tourist")
    @Expose
    public Tourist tourist;
    @SerializedName("guide")
    @Expose
    public Guide guide;
    @SerializedName("state")
    @Expose
    public VerificationState state;
    @SerializedName("routes")
    @Expose
    public List<TripRoute> routes;

    public Trip(){};

    protected Trip(Parcel in) {
        id = in.readInt();
        points = in.readInt();
        startDate = in.readString();
        endDate = in.readString();
        tourist = in.readParcelable(Place.class.getClassLoader());
        guide = in.readParcelable(Guide.class.getClassLoader());
        state = in.readParcelable(VerificationState.class.getClassLoader());
        routes = in.createTypedArrayList(TripRoute.CREATOR);
    }

    public static final Creator<Trip> CREATOR = new Creator<Trip>() {
        @Override
        public Trip createFromParcel(Parcel in) {
            return new Trip(in);
        }

        @Override
        public Trip[] newArray(int size) {
            return new Trip[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeInt(id);
        parcel.writeInt(points);
        parcel.writeString(startDate);
        parcel.writeString(endDate);
        parcel.writeParcelable(tourist, i);
        parcel.writeParcelable(guide, i);
        parcel.writeParcelable(state, i);
        parcel.writeTypedList(routes);
    }
}