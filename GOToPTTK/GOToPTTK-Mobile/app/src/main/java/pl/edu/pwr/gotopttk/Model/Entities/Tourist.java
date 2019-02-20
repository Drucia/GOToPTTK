package pl.edu.pwr.gotopttk.Model.Entities;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Tourist implements Parcelable {

    @SerializedName("user")
    @Expose
    public User user;

    protected Tourist(Parcel in) {
        user = in.readParcelable(User.class.getClassLoader());
    }

    public Tourist()
    {

    }

    public static final Creator<Tourist> CREATOR = new Creator<Tourist>() {
        @Override
        public Tourist createFromParcel(Parcel in) {
            return new Tourist(in);
        }

        @Override
        public Tourist[] newArray(int size) {
            return new Tourist[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeParcelable(user, i);
    }
}
