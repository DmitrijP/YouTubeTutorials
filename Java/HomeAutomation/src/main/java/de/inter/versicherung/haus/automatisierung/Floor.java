package de.inter.versicherung.haus.automatisierung;

import java.util.ArrayList;
import java.util.List;

public class Floor {
    private final int level;
    private List<Room> rooms;

    public Floor(int level) {
        this.level = level;
        rooms = new ArrayList<>();
    }

    public int getLevel() {
        return level;
    }

    public void addRoom(String roomName){
        Room r = new Room(roomName);
        rooms.add(r);
    }

    public Room findRoom(String roomName){
        for (int i = 0; i < rooms.size(); i++){
            Room r = rooms.get(i);
            if(r == null){
                continue;
            }
            if(roomName.equals(r.getRoomName())){
                return r;
            }
        }

        return new NullRoom();
    }

    @Override
    public String toString() {
        return "Floor{" +
                "level=" + level +
                ", rooms=" + rooms +
                '}';
    }
}
