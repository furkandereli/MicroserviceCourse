namespace MicroserviceCourse.Bus.Commands;

public record UploadCoursePictureCommand(Guid CourseId, Byte[] Picture, string FileName);
