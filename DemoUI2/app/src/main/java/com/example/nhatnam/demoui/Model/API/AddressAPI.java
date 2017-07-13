package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.District;
import com.example.nhatnam.demoui.Model.Province;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

/**
 * Created by NHAT NAM on 7/4/2017.
 */

public interface AddressAPI {
    @GET("api/Provinces")
    Call<List<Province>> getProvinces();
    @GET("api/Districts/{ProvinceID}")
    Call<List<District>> getDistricts(@Path("ProvinceID") int ProvinceID);
}
