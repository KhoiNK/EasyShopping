package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.Order;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

/**
 * Created by NHAT NAM on 6/28/2017.
 */

public interface OrderAPI {
    @POST("api/ShippingDetails/take_order/1")
    @FormUrlEncoded
    Call<Integer> takeOrder(@Field("OrderID") int OrderID,
                             @Field("ShipperID") int ShipperID);
    @GET("api/Orders/{StoreID}")
    Call<List<Order>> getOrder(@Path("StoreID") int StoreID);
}
