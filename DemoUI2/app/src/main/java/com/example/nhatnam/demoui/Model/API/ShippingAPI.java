package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.ShippingDetail;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.HTTP;
import retrofit2.http.POST;
import retrofit2.http.Path;

/**
 * Created by NHAT NAM on 7/11/2017.
 */

public interface ShippingAPI {
    @GET("api/shippingdetails/Approved/{UserID}")
    Call<List<ShippingDetail>> getApprovedOrder(@Path("UserID") int UserID);
    @GET("api/shippingdetails/request/{UserID}")
    Call<List<ShippingDetail>> getRequestOrder(@Path("UserID") int UserID);
    @FormUrlEncoded
    @HTTP(method = "DELETE", path = "api/shippingdetails/cancel/1", hasBody = true)
    Call<Integer> cancelOrder(@Field("OrderID") int OrderID,
                            @Field("ShipperID") int ShipperID);
    @POST("api/shippingdetails/completed/1")
    @FormUrlEncoded
    Call<Integer> completeOrder(@Field("OrderID") int OrderID,
                              @Field("ShipperID") int ShipperID);
}
