package de.dmitrij.patuk.insurance.backend.api.mapper;

import de.dmitrij.patuk.insurance.backend.api.dto.UserProfileCreationDto;
import de.dmitrij.patuk.insurance.backend.api.dto.UserProfileCreationResultDto;
import de.dmitrij.patuk.insurance.backend.data.entities.UserProfileEntity;
import org.springframework.web.bind.annotation.RequestParam;

public class UserProfileEntityToDtoMapper {
    public UserProfileCreationResultDto map(UserProfileEntity e){
        var userProfileDto = new UserProfileCreationResultDto();
        userProfileDto.setId(e.getId());
        userProfileDto.setUserName(e.getUserName());
        userProfileDto.setFirstName(e.getFirstName());
        userProfileDto.setLastName(e.getLastName());
        return userProfileDto;
    }

    public UserProfileEntity map(UserProfileCreationDto d){
        var userProfile = new UserProfileEntity();
        userProfile.setFirstName(d.getFirstName());
        userProfile.setLastName(d.getLastName());
        userProfile.setUserName(d.getUserName());
        return userProfile;
    }

    public UserProfileEntity map(String firstName,
                                  String lastName,
                                  String userName){
        var userProfile = new UserProfileEntity();
        userProfile.setFirstName(firstName);
        userProfile.setLastName(lastName);
        userProfile.setUserName(userName);
        return userProfile;
    }
}
