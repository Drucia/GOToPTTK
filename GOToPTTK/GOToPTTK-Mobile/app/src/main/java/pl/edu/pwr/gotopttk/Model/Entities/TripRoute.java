package pl.edu.pwr.gotopttk.Model.Entities;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class TripRoute implements Parcelable {

    @SerializedName("id")
    @Expose
    public int id;
    @SerializedName("route")
    @Expose
    public Route route;
    @SerializedName("state")
    @Expose
    public VerificationState state;

    protected TripRoute(Parcel in) {
        id = in.readInt();
        route = in.readParcelable(Route.class.getClassLoader());
        state = in.readParcelable(VerificationState.class.getClassLoader());
    }

    public static final Creator<TripRoute> CREATOR = new Creator<TripRoute>() {
        @Override
        public TripRoute createFromParcel(Parcel in) {
            return new TripRoute(in);
        }

        @Override
        public TripRoute[] newArray(int size) {
            return new TripRoute[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeInt(id);
        parcel.writeParcelable(route, i);
        parcel.writeParcelable(state, i);
    }


}