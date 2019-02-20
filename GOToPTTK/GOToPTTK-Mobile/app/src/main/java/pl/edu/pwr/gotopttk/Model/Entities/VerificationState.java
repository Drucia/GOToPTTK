package pl.edu.pwr.gotopttk.Model.Entities;

import android.os.Parcel;
import android.os.Parcelable;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class VerificationState implements Parcelable {

    @SerializedName("state")
    @Expose
    public String state;

    protected VerificationState(Parcel in) {
        state = in.readString();
    }

    public static final Creator<VerificationState> CREATOR = new Creator<VerificationState>() {
        @Override
        public VerificationState createFromParcel(Parcel in) {
            return new VerificationState(in);
        }

        @Override
        public VerificationState[] newArray(int size) {
            return new VerificationState[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(state);
    }
}