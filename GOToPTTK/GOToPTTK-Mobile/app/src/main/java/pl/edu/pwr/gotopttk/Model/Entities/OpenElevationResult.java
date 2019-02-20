package pl.edu.pwr.gotopttk.Model.Entities;


import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class OpenElevationResult {

    @SerializedName("results")
    @Expose
    public List<Result> results = null;


    public class Result {

        @SerializedName("longitude")
        @Expose
        public double longitude;
        @SerializedName("latitude")
        @Expose
        public double latitude;

        @SerializedName("elevation")
        @Expose
        public double elevation;

    }


}
