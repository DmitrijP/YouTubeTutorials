package de.dmitrij.patuk.insurance.backend.api.controller;

import de.dmitrij.patuk.insurance.backend.api.dto.UserProfileCreationResultDto;
import de.dmitrij.patuk.insurance.backend.api.mapper.UserProfileEntityToDtoMapper;
import de.dmitrij.patuk.insurance.backend.data.repositories.UserProfileRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.List;
import java.util.stream.Collectors;

@Controller
@RequestMapping("/v1/user-profile")
public class UserProfileWebController {

    private final UserProfileRepository userProfileRepository;
    private final UserProfileEntityToDtoMapper mapper;

    @Autowired
    public UserProfileWebController(UserProfileRepository userProfileRepository,
                                 UserProfileEntityToDtoMapper mapper) {
        this.userProfileRepository = userProfileRepository;
        this.mapper = mapper;
    }

    @GetMapping("/index")
    public String index(){
        return "/userprofile/index";
    }

    @GetMapping("all")
    public String findProfiles(Model model)  {
        var foundProfileOpt = userProfileRepository.findAll();
        var mappedDtos = foundProfileOpt.stream()
                .map(mapper::map)
                .collect(Collectors.toList());
        model.addAttribute("profiles", mappedDtos);
        return "/userprofile/all-profiles";
    }
}
