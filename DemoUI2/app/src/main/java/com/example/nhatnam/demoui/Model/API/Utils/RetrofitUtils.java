package com.example.nhatnam.demoui.Model.API.Utils;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by NHAT NAM on 6/15/2017.
 */

public class RetrofitUtils {
    private static Retrofit retrofit = null;

    public static Retrofit get() {
        if (retrofit==null) {
            retrofit = new Retrofit.Builder()
                    .baseUrl(Constant.BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
        return retrofit;
    }

}
