package com.example.nhatnam.demoui.Presenter.Activity.Adapter;

import com.example.nhatnam.demoui.Model.Shop;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by NHAT NAM on 6/12/2017.
 */

public class ShopAdapter {
    private List<Shop> shops;
    private List<Marker> listMaker;
    private int i;
    public ShopAdapter(List<Shop> shops) {
        this.shops = shops;
        listMaker=new ArrayList<>();
    }
    public void addMaker(GoogleMap map){
        int size = shops.size();
//        map.clear();
        if(listMaker.size()>0){
            for(Marker marker:listMaker){
                marker.remove();
            }
        }
        for (i=0;i<size; i++){
            listMaker.add(map.addMarker(new MarkerOptions()
                    .position(new LatLng(shops.get(i).getLatitude(),shops.get(i).getLongitude()))));
//            map.addMarker(new MarkerOptions()
//                    .position(new LatLng(shops.get(i).getLatitude(),shops.get(i).getLongitude())));
        }
    }
    public void removeMaker(GoogleMap map){
        for(int i=0;i<listMaker.size();i++){
        }
    }
    public int findShopByLatlng(LatLng latLng){
        for(int i=0;i<shops.size();i++){
            if(new LatLng(shops.get(i).getLatitude(),shops.get(i).getLongitude()).equals(latLng))
                return i;
        }
        return -1;
    }
}
