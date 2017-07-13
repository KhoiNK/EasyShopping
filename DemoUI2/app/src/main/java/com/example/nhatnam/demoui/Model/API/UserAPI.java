package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.User;

import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.POST;

/**
 * Created by NHAT NAM on 6/23/2017.
 */

public interface UserAPI {
    @POST("api/Users/login")
    @FormUrlEncoded
    Call<User> login(@Field("UserName") String UserName,
                          @Field("PassWord") String PassWord);
}
