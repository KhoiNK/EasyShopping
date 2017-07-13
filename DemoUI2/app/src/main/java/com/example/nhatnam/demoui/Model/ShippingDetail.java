package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by NHAT NAM on 7/11/2017.
 */

public class ShippingDetail {
    @SerializedName("ShopLat")
    private double ShopLat;
    @SerializedName("ShopLng")
    private double ShopLng;
    @SerializedName("OrderStatus")
    private int OrderStatus;
    @SerializedName("StoreName")
    private String StoreName;
    @SerializedName("Order")
    private Order order;
    @SerializedName("ModifiedID")
    private int ModifiedID;

    public int getModifiedID() {
        return ModifiedID;
    }

    public double getShopLat() {
        return ShopLat;
    }

    public double getShopLng() {
        return ShopLng;
    }

    public int getOrderStatus() {
        return OrderStatus;
    }

    public String getStoreName() {
        return StoreName;
    }

    public Order getOrder() {
        return order;
    }
}
