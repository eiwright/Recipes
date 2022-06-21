namespace Recipe.Service.Business.Mappers;

public static class InstructionExtension
{
    public static Interfaces.DTO.Instruction ToDTO(this Domain.Models.Instruction instruction)
    {
        if (instruction == null) return null;

        return new Interfaces.DTO.Instruction
        {
            Id = instruction.Id,
            Description = instruction.Description,
            Order = instruction.Order,
        };
    }

    public static Domain.Models.Instruction ToDomain(this Interfaces.DTO.Instruction instruction)
    {
        if (instruction == null) return null;

        return new Domain.Models.Instruction
        {
            Id = instruction.Id,
            Description = instruction.Description,
            Order = instruction.Order,
        };
    }
}
