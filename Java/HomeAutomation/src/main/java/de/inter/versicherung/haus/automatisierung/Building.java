package de.inter.versicherung.haus.automatisierung;

import java.util.Arrays;

public class Building {
    private final String city;
    private final String street;
    private final int streetNumber;
    private final Floor[] floors;

    public Building(String city, String street, int streetNumber, int levels) {
        this.city = city;
        this.street = street;
        this.streetNumber = streetNumber;
        floors = new Floor[levels];

        generateNeededFloors(levels);
    }

    private void generateNeededFloors(int levels) {
        for (int i = 0; i < levels; i++){
            floors[i] = new Floor(i);
        }
    }


    public String getCity() {
        return city;
    }

    public String getStreet() {
        return street;
    }

    public int getStreetNumber() {
        return streetNumber;
    }

    public int getFloorCount(){
        return floors.length;
    }

    public Floor getFloor(int level){
        if(level > floors.length){
            return new NullFloor();
        }
        return floors[level];
    }

    @Override
    public String toString() {
        return "Building{" +
                "city='" + city + '\'' +
                ", street='" + street + '\'' +
                ", streetNumber=" + streetNumber +
                ", floors=" + Arrays.toString(floors) +
                '}';
    }
}
