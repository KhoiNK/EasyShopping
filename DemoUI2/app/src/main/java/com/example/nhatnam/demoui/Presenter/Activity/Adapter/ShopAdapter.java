package com.example.nhatnam.demoui.Presenter.Activity.Adapter;

import com.example.nhatnam.demoui.Model.Shop;
import com.example.nhatnam.demoui.Presenter.Activity.MainActivity;
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
    private static final int FIND_NEAR = 1;
    private static final int SEARCH = 2;
    public static int STATUS = FIND_NEAR;
    private static int RADIUS = 1;
    private List<Shop> shops;
//    private List<Marker> listMaker;
    private int i;

    public ShopAdapter(List<Shop> shops) {
        this.shops = shops;
//        listMaker = new ArrayList<>();
    }

    public List<Marker> addMaker(GoogleMap map,List<Marker> list) {
        List<Shop> listShop;
        if(STATUS==FIND_NEAR){
            listShop = ListShopNear(shops);
        }else listShop = shops;
//        map.clear();
        if (list.size() > 0) {
            for (Marker marker : list) {
                marker.remove();
            }
        }
        for (i = 0; i < listShop.size(); i++) {
            list.add(map.addMarker(new MarkerOptions()
                    .position(new LatLng(listShop.get(i).getLatitude(), listShop.get(i).getLongitude()))));
        }
        return list;
    }

    private List<Shop> ListShopNear(List<Shop> list) {
        LatLng myLatLng = MainActivity.myLatlng;
        List<Shop> nearShop = new ArrayList<>();
        double minLat = getDestinationPoint(myLatLng, 180, RADIUS).latitude;
        double maxLat = getDestinationPoint(myLatLng, 0, RADIUS).latitude;
        double minlng = getDestinationPoint(myLatLng, 270, RADIUS).longitude;
        double maxlng = getDestinationPoint(myLatLng, 90, RADIUS).longitude;
        for (Shop shop : list) {
            if (shop.getLatitude() >= minLat && shop.getLatitude() <= maxLat
                    && shop.getLongitude() >= minlng && shop.getLongitude() <= maxlng) {
                if (Distance(shop.getLatitude(), shop.getLongitude(), myLatLng.latitude, myLatLng.longitude) <= 1) {
                    nearShop.add(shop);
                }
            } else {
                if (shop.getLongitude() > maxlng)
                    break;
            }
        }
        return nearShop;
    }

    public int findShopByLatlng(LatLng latLng) {
        for (int i = 0; i < shops.size(); i++) {
            if (new LatLng(shops.get(i).getLatitude(), shops.get(i).getLongitude()).equals(latLng))
                return i;
        }
        return -1;
    }

    private Double Distance(double lat_a, double lng_a, double lat_b, double lng_b) {
        double earthRadius = 3958.75;
        double latDiff = Math.toRadians(lat_b - lat_a);
        double lngDiff = Math.toRadians(lng_b - lng_a);
        double a = Math.sin(latDiff / 2) * Math.sin(latDiff / 2) +
                Math.cos(Math.toRadians(lat_a)) * Math.cos(Math.toRadians(lat_b)) *
                        Math.sin(lngDiff / 2) * Math.sin(lngDiff / 2);
        double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        double distance = earthRadius * c;

        int meterConversion = 1609;

        return new Double(distance * meterConversion / 1000);
    }

    private LatLng getDestinationPoint(LatLng source, double brng, double dist) {
        dist = dist / 6371;
        brng = Math.toRadians(brng);
        double lat1 = Math.toRadians(source.latitude), lon1 = Math.toRadians(source.longitude);
        double lat2 = Math.asin(Math.sin(lat1) * Math.cos(dist) +
                Math.cos(lat1) * Math.sin(dist) * Math.cos(brng));
        double lon2 = lon1 + Math.atan2(Math.sin(brng) * Math.sin(dist) *
                        Math.cos(lat1),
                Math.cos(dist) - Math.sin(lat1) *
                        Math.sin(lat2));
        if (Double.isNaN(lat2) || Double.isNaN(lon2)) {
            return null;
        }
        return new LatLng(Math.toDegrees(lat2), Math.toDegrees(lon2));
    }
}
