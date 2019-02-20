package pl.edu.pwr.gotopttk.Model.Entities;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Guide implements Parcelable {

    @SerializedName("user")
    @Expose
    public User user;

    protected Guide(Parcel in) {
        user = in.readParcelable(User.class.getClassLoader());
    }

    public Guide(){};

    public static final Creator<Guide> CREATOR = new Creator<Guide>() {
        @Override
        public Guide createFromParcel(Parcel in) {
            return new Guide(in);
        }

        @Override
        public Guide[] newArray(int size) {
            return new Guide[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i)
    {
        parcel.writeParcelable(user, i);
    }
}