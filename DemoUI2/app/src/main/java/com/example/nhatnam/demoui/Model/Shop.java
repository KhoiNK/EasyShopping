package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

/**
 * Created by NHAT NAM on 6/12/2017.
 */

public class Shop implements Serializable {
//    @SerializedName("id")
//    @Expose
//    private int shopID;
    @SerializedName("Latitude")
    @Expose
    private double latitude;
    @SerializedName("Longitude")
    @Expose
    private double longitude;
    @SerializedName("Orders")
    @Expose
    private List<Order> listOrder;

//    public Shop(int shopID, LatLng position) {
//        this.shopID = shopID;
//        this.latitude = position.latitude;
//        this.longitude = position.longitude;
////        Name = name;
//    }

//    public int getShopID() {
//        return shopID;
//    }
//
//    public void setShopID(int shopID) {
//        this.shopID = shopID;
//    }

    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    public double getLongitude() {
        return longitude;
    }

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }

//    public String getName() {
//        return Name;
//    }
//
//    public void setName(String name) {
//        Name = name;
//    }
//
    public List<Order> getListOrder() {
        return listOrder;
    }

    public void setListOrder(List<Order> listOrder) {
        this.listOrder = listOrder;
    }
}
