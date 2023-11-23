package de.dmitrij.patuk.insurance.backend.api.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping("/v1/home")
public class HomeController {

    @GetMapping("/index")
    public String index(){
        return "/index";
    }
}
