package com.example.nhatnam.demoui.Model;

import com.example.nhatnam.demoui.Model.API.DistanceFinderListener;
import com.example.nhatnam.demoui.Presenter.Activity.DistanceFinder;

import java.io.UnsupportedEncodingException;
import java.util.List;

/**
 * Created by NHAT NAM on 7/22/2017.
 */

public class Point {
    private String from;
    private String destination;
    private double Distance;
    public boolean isEnable;

    public Point(String from) {
        this.from = from;
    }

    public Point(String from, String destination) {
        this.from = from;
        this.destination= destination;
        isEnable=false;
//        isEnable=true;
    }
    public Point(Point point) {
          this.from=point.getFrom();
          this.destination=point.getDestination();
        this.Distance=point.getDistance();
        this.isEnable=point.isEnable;
    }
    public String getFrom() {
        return from;
    }

    public void setFrom(String from) {
        this.from = from;
    }

    public String getDestination() {
        return destination;
    }

    public double getDistance() {
        return Distance;
    }

    public void setDestination(String destination) {
        this.destination = destination;
    }

    public void setDistance(double distance) {
        Distance = distance;
    }

    public void findDistance(){
        if(from.equals(destination)){
            setDistance(0);
        }else{
            try {
                new DistanceFinder(new DistanceFinderListener() {
                    @Override
                    public void onDirectionFinderSuccess(List<Route> route) {
                        Distance= route.get(0).distance.value;
                    }
                }, from, destination).execute();
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
            }
        }
    }

//    @Override
//    public void onDirectionFinderStart() {
//
//    }
//
//    @Override
//    public void onDirectionFinderSuccess(List<Route> route) {
//        Distance= route.get(0).distance.value;
//    }
}
