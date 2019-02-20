package pl.edu.pwr.gotopttk.View.Views;


import android.os.Parcel;

import com.arlib.floatingsearchview.suggestions.model.SearchSuggestion;

import pl.edu.pwr.gotopttk.Model.Entities.Place;

public class PlaceSearchSuggestion implements SearchSuggestion {

    private Place place;

    public PlaceSearchSuggestion(Place place) {
        this.place = place;
    }

    /**
     * Returns the text that should be displayed
     * for the suggestion represented by this object.
     *
     * @return the text for this suggestion
     */
    @Override
    public String getBody() {
        return place.name;
    }


    @Override
    public int describeContents() {
        return 0;
    }



    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeParcelable(this.place, flags);
    }

    protected PlaceSearchSuggestion(Parcel in) {
        this.place = in.readParcelable(Place.class.getClassLoader());
    }

    public static final Creator<PlaceSearchSuggestion> CREATOR = new Creator<PlaceSearchSuggestion>() {
        @Override
        public PlaceSearchSuggestion createFromParcel(Parcel source) {
            return new PlaceSearchSuggestion(source);
        }

        @Override
        public PlaceSearchSuggestion[] newArray(int size) {
            return new PlaceSearchSuggestion[size];
        }
    };
}
