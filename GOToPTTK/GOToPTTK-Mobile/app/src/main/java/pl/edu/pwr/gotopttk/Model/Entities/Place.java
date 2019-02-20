package pl.edu.pwr.gotopttk.Model.Entities;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Place implements Parcelable {

    public static final int NO_ID = -1;

    @SerializedName("id")
    @Expose
    public int id;
    @SerializedName("name")
    @Expose
    public String name;
    @SerializedName("longitude")
    @Expose
    public double longitude;
    @SerializedName("latitude")
    @Expose
    public double latitude;
    @SerializedName("altitude")
    @Expose
    public double altitude;

    public Place(String name, double longitude, double latitude, double altitude) {
        this.id = NO_ID;
        this.name = name;
        this.longitude = longitude;
        this.latitude = latitude;
        this.altitude = altitude;
    }

    public Place(int id, String name, double longitude, double latitude, double altitude) {
        this.id = id;
        this.name = name;
        this.longitude = longitude;
        this.latitude = latitude;
        this.altitude = altitude;
    }

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeInt(this.id);
        dest.writeString(this.name);
        dest.writeDouble(this.longitude);
        dest.writeDouble(this.latitude);
        dest.writeDouble(this.altitude);
    }

    public Place() {
    }

    @Override
    public String toString() {
        if(name == null){
            return String.format("%f,%f", latitude, longitude);
        }
        else {
            return name;
        }
    }

    protected Place(Parcel in) {
        this.id = in.readInt();
        this.name = in.readString();
        this.longitude = in.readDouble();
        this.latitude = in.readDouble();
        this.altitude = in.readDouble();
    }

    public static final Parcelable.Creator<Place> CREATOR = new Parcelable.Creator<Place>() {
        @Override
        public Place createFromParcel(Parcel source) {
            return new Place(source);
        }

        @Override
        public Place[] newArray(int size) {
            return new Place[size];
        }
    };
}
