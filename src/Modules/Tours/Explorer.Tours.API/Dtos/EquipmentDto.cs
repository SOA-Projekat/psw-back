﻿namespace Explorer.Tours.API.Dtos;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TourId { get; set; }
}