using SocialMed.API.Reports.Domain.Models;
using SocialMed.API.Reports.Domain.Repositories;
using SocialMed.API.Reports.Domain.Services;
using SocialMed.API.Reports.Domain.Services.Communication;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Shared.Domain.Repositories;

namespace SocialMed.API.Reports.Services;

public class ReportService: IReportService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository, 
        IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _reportRepository = reportRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await  _reportRepository.ListAsync();
    }

    public async Task<IEnumerable<Report>> ListByUserIdAsync(int userId)
    {
        return await _reportRepository.ListByUserIdAsync(userId);
    }

    public async Task<ReportResponse> GetByIdAsync(int id)
    {
        var report = await _reportRepository.FindByIdAsync(id);
        if (report == null)
            return new ReportResponse("Report doesn't exist.");
        return new ReportResponse(report);
    }

    public async Task<ReportResponse> SaveAsync(Report report)
    {
        // Validate user id

        var existingUser = await _userRepository.FindByIdAsync(report.UserId);

        if (existingUser == null)
            return new ReportResponse("Invalid User");

        try
        {
            // Add Tutorial
            await _reportRepository.AddAsync(report);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new ReportResponse(report);

        }
        catch (Exception e)
        {
            // Error Handling
            return new ReportResponse($"An error occurred while saving the new report: {e.Message}");
        }
    }
    
    public async Task<ReportResponse> DeleteAsync(int id)
    {
        var existingReport = await _reportRepository.FindByIdAsync(id);
        
        // Validate report

        if (existingReport== null)
            return new ReportResponse("Report not found.");
        
        try
        {
            _reportRepository.Remove(existingReport);
            await _unitOfWork.CompleteAsync();

            return new ReportResponse(existingReport);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ReportResponse($"An error occurred while deleting the Report: {e.Message}");
        }
    }
}