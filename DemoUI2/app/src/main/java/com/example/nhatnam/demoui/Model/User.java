package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by NHAT NAM on 6/18/2017.
 */

public class User {
    @SerializedName("ID")
    private int ID;
    @SerializedName("UserName")
    private String UserName;
//    @SerializedName("Password")
//    private String Password;
    @SerializedName("IsShipper")
    private boolean IsShipper;
    @SerializedName("FirstName")
    private String FirstName;
    @SerializedName("LastName")
    private String LastName;
    @SerializedName("BirthDay")
    private Date BirthDay;
    @SerializedName("StartDate")
    private Date StartDate;
    @SerializedName("Phone")
    private String Phone;
    @SerializedName("Total")
    private Double Total;
    @SerializedName("CurrentDeposit")
    private Double CurrentDeposit;
    @SerializedName("Deposit")
    private Double Deposit;
    @SerializedName("UserImage")
    private String UserImage;

    public User(int ID, String userName) {
        this.ID = ID;
        UserName = userName;
    }

    public int getID() {
        return ID;
    }


    public String getUserName() {
        return UserName;
    }

    public boolean isShipper() {
        return IsShipper;
    }

    public String getFirstName() {
        return FirstName;
    }

    public String getLastName() {
        return LastName;
    }

    public Date getBirthDay() {
        return BirthDay;
    }

    public Date getStartDate() {
        return StartDate;
    }

    public String getPhone() {
        return Phone;
    }

    public Double getTotal() {
        return Total;
    }

    public Double getCurrentDeposit() {
        return CurrentDeposit;
    }

    public Double getDeposit() {
        return Deposit;
    }

    public String getUserImage() {
        return UserImage;
    }
}
