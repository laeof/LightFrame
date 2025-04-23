using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RoomRepository : IRoomRepository
{
    private AppDbContext context;
    public RoomRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IResult<Room>> AddRoomAsync(Room room)
    {
        try
        {
            context.Entry(room).State = EntityState.Added;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<Room>.Faillure(new("500", ex.Message));
        }

        return Result<Room>.Success(room);
    }

    public async Task<IResult<Room>> GetRoomByIdAsync(Guid id)
    {
        var roomResult = await context.Rooms.FirstOrDefaultAsync(u => u.Id == id);

        if (roomResult == null) return Result<Room>.Faillure(new("404", "Room not found"));

        return Result<Room>.Success(roomResult);
    }

    public async Task<IResult<List<Room>>> GetRoomsAsync()
    {
        var roomResult = await context.Rooms.ToListAsync();

        if (roomResult == null) return Result<List<Room>>.Faillure(new("404", "Room not found"));

        return Result<List<Room>>.Success(roomResult);
    }

    public async Task<IResult<Room>> UpdateRoomAsync(Room room)
    {
        try
        {
            context.Entry(room).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<Room>.Faillure(new("500", ex.Message));
        }

        return Result<Room>.Success(room);
    }
}