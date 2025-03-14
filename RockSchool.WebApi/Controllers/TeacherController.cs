﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AvailablePeriodsService;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Teachers;

namespace RockSchool.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : Controller
{
    private readonly ITeacherService _teacherService;
    private readonly IUserService _userService;
    private readonly IDisciplineService _disciplineService;
    private readonly IAvailablePeriodsService _availablePeriodsService;

    public TeacherController(ITeacherService teacherService, IUserService userService,
        IDisciplineService disciplineService, IAvailablePeriodsService availablePeriodsService)
    {
        _userService = userService;
        _disciplineService = disciplineService;
        _availablePeriodsService = availablePeriodsService;
        _teacherService = teacherService;
    }

    [EnableCors("MyPolicy")]
    [HttpPost]
    public async Task<ActionResult> AddTeacher([FromBody] RegisterTeacherRequestDto requestDto)
    {
        if (!ModelState.IsValid)
            throw new Exception("Incorrect requestDto for registration.");

        var newTeacher = new TeacherDto
        {
            FirstName = requestDto.Teacher.FirstName,
            LastName = requestDto.Teacher.LastName,
            BirthDate = requestDto.Teacher.BirthDate.ToUniversalTime(),
            Phone = requestDto.Teacher.Phone,
            BranchId = requestDto.Teacher.BranchId,
            Sex = requestDto.Teacher.Sex,
            DisciplineIds = requestDto.Teacher.Disciplines,
            WorkingPeriods = requestDto.WorkingPeriods,
            AllowGroupLessons = requestDto.Teacher.AllowGroupLessons,
            AgeLimit = requestDto.Teacher.AgeLimit
        };

        await _teacherService.AddTeacher(newTeacher);

        return Ok();
    }

    [EnableCors("MyPolicy")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] TeacherFormDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updateRequest = new TeacherDto()
        {
            TeacherId = id,
            FirstName = model.Teacher.FirstName,
            LastName = model.Teacher.LastName,
            BirthDate = model.Teacher.BirthDate,
            Sex = model.Teacher.Sex,
            Phone = model.Teacher.Phone,
            AgeLimit = model.Teacher.AgeLimit,
            AllowGroupLessons = model.Teacher.AllowGroupLessons,
            DisciplineIds = model.Teacher.Disciplines
        };

        await _teacherService.UpdateTeacherAsync(updateRequest);

        return Ok();
    }

    [EnableCors("MyPolicy")]
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        var result = new TeacherInfo
        {
            Email = teacher.User?.Login,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            BirthDate = teacher.BirthDate,
            Sex = teacher.Sex,
            Phone = teacher.Phone,
            Disciplines = teacher.Disciplines?.Select(d => d.DisciplineId).ToArray(),
            AllowGroupLessons = teacher.AllowGroupLessons,
            AgeLimit = teacher.AgeLimit,
            BranchId = teacher.BranchId,
        };

        return Ok(result);
    }

    [EnableCors("MyPolicy")]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();

        if (teachers.Length == 0) return NotFound();

        return Ok(teachers);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("getTeacherScreenDetails/{id}")]
    public async Task<ActionResult> GetTeacherScreenDetails(Guid id)
    {
        var teacherDto = await _teacherService.GetTeacherByIdAsync(id);

        var teacherScreenDetailsDto = new TeacherScreenDetailsDto
        {
            Teacher = teacherDto,
            Subscriptions = new List<string>(),
        };

        return Ok(teacherScreenDetailsDto);
    }

    [EnableCors("MyPolicy")]
    [HttpGet("getAvailablePeriods")]
    public async Task<ActionResult> GetAvailablePeriods(int disciplineId, Guid studentId, int branchId)
    {
        // TODO: refactor.
        var periods = await _availablePeriodsService.GetAvailablePeriodsAsync(disciplineId, branchId);

        return Ok(periods);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _teacherService.DeleteTeacherAsync(id);

        return Ok();
    }
}