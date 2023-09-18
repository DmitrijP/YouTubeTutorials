package de.inter.versicherung.haus.automatisierung;

import java.util.ArrayList;
import java.util.List;

public class Room {
    private final String roomName;
    private final Thermostat thermostat;
    private final List<Light> lights;
    private final List<Door> doors;
    private final List<Window> windows;

    public Room() {
        roomName = "undefined";
        thermostat = new Thermostat();
        lights = new ArrayList<>();
        doors = new ArrayList<>();
        windows = new ArrayList<>();
    }

    public Room(String roomName) {
        this.roomName = roomName;
        thermostat = new Thermostat();
        lights = new ArrayList<>();
        doors = new ArrayList<>();
        windows = new ArrayList<>();
    }

    public String getRoomName() {
        return roomName;
    }

    //===========> Lights

    public void addLight(String lightName){
        lights.add(new Light(lightName));
    }

    public boolean toggleLightState(String lightName){
        for (int i = 0; i < lights.size(); i++){
            Light l = lights.get(i);
            if(l == null){
                continue;
            }
            if(lightName.equals(l.getLightName())){
                l.toggleOnState();
                return true;
            }
        }
        return false;
    }

    public boolean setLightState(String lightName, boolean newIsOnState){
        for (int i = 0; i < lights.size(); i++){
            Light l = lights.get(i);
            if(l == null){
                continue;
            }
            if(lightName.equals(l.getLightName())){
                if(newIsOnState != l.getState()){
                    l.toggleOnState();
                }
                return true;
            }
        }
        return false;
    }

    //===========> Doors
    public void addDoor(String doorName){
        doors.add(new Door(doorName));
    }

    public boolean toggleDoorState(String doorName){
        for (int i = 0; i < doors.size(); i++){
            Door l = doors.get(i);
            if(l == null){
                continue;
            }
            if(doors.equals(l.getName())){
                l.toggleOpen();
                return true;
            }
        }
        return false;
    }

    public boolean setDoorState(String doorName, boolean newIsOpenState){
        for (int i = 0; i < doors.size(); i++){
            Door l = doors.get(i);
            if(l == null){
                continue;
            }
            if(doorName.equals(l.getName())){
                if(newIsOpenState != l.getIsOpen()){
                    l.toggleOpen();
                }
                return true;
            }
        }
        return false;
    }


    //===========> Windows
    public void addWindow(String name){
        windows.add(new Window(name));
    }

    public boolean toggleWindowState(String name){
        for (int i = 0; i < windows.size(); i++){
            Window l = windows.get(i);
            if(l == null){
                continue;
            }
            if(windows.equals(l.getName())){
                l.toggleOpen();
                return true;
            }
        }
        return false;
    }

    public boolean setWindowState(String name, boolean newIsOpenState){
        for (int i = 0; i < windows.size(); i++){
            Window l = windows.get(i);
            if(l == null){
                continue;
            }
            if(name.equals(l.getName())){
                if(newIsOpenState != l.getIsOpen()){
                    l.toggleOpen();
                }
                return true;
            }
        }
        return false;
    }

    @Override
    public String toString() {
        return "Room{" +
                "roomName='" + roomName + '\'' +
                ", thermostat=" + thermostat +
                ", lights=" + lights +
                ", doors=" + doors +
                ", windows=" + windows +
                '}';
    }
}
