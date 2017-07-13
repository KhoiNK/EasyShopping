package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by NHAT NAM on 6/18/2017.
 */

public class OrderDetail implements Serializable {
//    @SerializedName("OrderID")
//    @Expose
//    private int orderID;
    @SerializedName("CreatedDate")
    @Expose
    private String createDate;
//    @SerializedName("ProductID")
//    @Expose
//    private int productID;
//    @SerializedName("Quantity")
//    @Expose
//    private int quantity;
    @SerializedName("Price")
    @Expose
    private double price;
    @SerializedName("Weight")
    @Expose
    private double Weight;
//    public int getOrderID() {
//        return orderID;
//    }
//
//    public void setOrderID(int orderID) {
//        this.orderID = orderID;
//    }

    public String getCreateDate() {
        return createDate;
    }

    public void setCreateDate(String createDate) {
        this.createDate = createDate;
    }

//    public int getProductID() {
//        return productID;
//    }
//
//    public void setProductID(int productID) {
//        this.productID = productID;
//    }
//
//    public int getQuantity() {
//        return quantity;
//    }
//
//    public void setQuantity(int quantity) {
//        this.quantity = quantity;
//    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public double getWeight() {
        return Weight;
    }

    public void setWeight(double weight) {
        Weight = weight;
    }
}
