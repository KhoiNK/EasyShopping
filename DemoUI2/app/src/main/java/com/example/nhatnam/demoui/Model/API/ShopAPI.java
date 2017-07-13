package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.Shop;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.POST;

/**
 * Created by NHAT NAM on 6/15/2017.
 */

public interface ShopAPI {
    @POST("api/Orders/search_order")
    @FormUrlEncoded
    Call<List<Shop>> getShops(@Field("ShipperID") int ShipperID,
                              @Field("FromProvince") int FromProvince,
                              @Field("FromDistrict") int FromDistrict,
                              @Field("ToProvince") int ToProvince,
                              @Field("ShipperID") int ToDistrict);
}
