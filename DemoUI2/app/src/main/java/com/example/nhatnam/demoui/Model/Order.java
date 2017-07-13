package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by NHAT NAM on 6/3/2017.
 */

public class Order implements Serializable {
    @SerializedName("ID")
    @Expose
    private int ID;
    @SerializedName("OrderCode")
    @Expose
    private String orderCode;
    @SerializedName("Address")
    @Expose
    private String address;
    @SerializedName("OrderDetails")
    @Expose
    private OrderDetail orderDetail;
    @SerializedName("Total")
    @Expose
    private Double Total;
    public boolean isChecked = false;

    public String getOrderCode() {
        return orderCode;
    }

    public void setOrderCode(String orderCode) {
        this.orderCode = orderCode;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public OrderDetail getOrderDetail() {
        return orderDetail;
    }

    public void setOrderDetail(OrderDetail orderDetail) {
        this.orderDetail = orderDetail;
    }

    public Double getTotal() {
        return Total;
    }

    public void setTotal(Double total) {
        Total = total;
    }

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public void check() {
        if (isChecked) {
            isChecked = false;
        } else isChecked = true;
    }
}
