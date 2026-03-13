using EF_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadAllData();
        }

        private void LoadAllData()
        {
            LoadDepartments();
            LoadInstructors();
            LoadStudents();
            LoadCourses();
        }

        #region Departments
        private void LoadDepartments()
        {
            using var context = new MyDbContext();
            dgvDepartments.DataSource = context.Departments
                .Select(d => new { d.DepartmentId, d.Name, d.Location, d.ManagerId })
                .ToList();
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
            {
                MessageBox.Show("Please enter department name.");
                return;
            }

            using var context = new MyDbContext();
            var department = new Department
            {
                Name = txtDepartmentName.Text,
                Location = txtDepartmentLocation.Text
            };
            context.Departments.Add(department);
            context.SaveChanges();
            LoadAllData();
            ClearDepartmentFields();
        }

        private void dgvDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count == 0) return;

            var row = dgvDepartments.SelectedRows[0];
            txtDepartmentName.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
            txtDepartmentLocation.Text = row.Cells["Location"].Value?.ToString() ?? string.Empty;
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count == 0) return;

            var id = (int)dgvDepartments.SelectedRows[0].Cells["DepartmentId"].Value;
            using var context = new MyDbContext();

            var hasInstructors = context.Instructors.Any(i => i.DepartmentId == id);
            var hasCourses = context.Courses.Any(c => c.DepartmentId == id);

            if (hasInstructors || hasCourses)
            {
                MessageBox.Show("Cannot delete this department because it is referenced by instructors or courses.");
                return;
            }

            var dept = context.Departments.Find(id);
            if (dept != null)
            {
                context.Departments.Remove(dept);
                context.SaveChanges();
                LoadAllData();
            }
        }

        private void btnUpdateDepartment_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a department to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
            {
                MessageBox.Show("Please enter department name.");
                return;
            }

            var id = (int)dgvDepartments.SelectedRows[0].Cells["DepartmentId"].Value;
            using var context = new MyDbContext();
            var department = context.Departments.Find(id);
            if (department == null) return;

            department.Name = txtDepartmentName.Text;
            department.Location = txtDepartmentLocation.Text;

            context.SaveChanges();
            LoadAllData();
            ClearDepartmentFields();
        }

        private void ClearDepartmentFields()
        {
            txtDepartmentName.Clear();
            txtDepartmentLocation.Clear();
        }
        #endregion

        #region Instructors
        private void LoadInstructors()
        {
            using var context = new MyDbContext();
            dgvInstructors.DataSource = context.Instructors
                .Include(i => i.Department)
                .Select(i => new { i.Id, i.FirstName, i.LastName, i.Phone, Department = i.Department.Name })
                .ToList();

            cmbInstructorDepartment.DataSource = context.Departments.ToList();
            cmbInstructorDepartment.DisplayMember = "Name";
            cmbInstructorDepartment.ValueMember = "DepartmentId";
        }

        private void btnAddInstructor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInstructorFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtInstructorLastName.Text))
            {
                MessageBox.Show("Please enter first and last name.");
                return;
            }

            using var context = new MyDbContext();
            var instructor = new Instructor
            {
                FirstName = txtInstructorFirstName.Text,
                LastName = txtInstructorLastName.Text,
                Phone = txtInstructorPhone.Text,
                DepartmentId = (int)cmbInstructorDepartment.SelectedValue!
            };
            context.Instructors.Add(instructor);
            context.SaveChanges();
            LoadAllData();
            ClearInstructorFields();
        }

        private void dgvInstructors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInstructors.SelectedRows.Count == 0) return;

            var row = dgvInstructors.SelectedRows[0];
            txtInstructorFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? string.Empty;
            txtInstructorLastName.Text = row.Cells["LastName"].Value?.ToString() ?? string.Empty;
            txtInstructorPhone.Text = row.Cells["Phone"].Value?.ToString() ?? string.Empty;

            var id = (int)row.Cells["Id"].Value;
            using var context = new MyDbContext();
            var instructor = context.Instructors.AsNoTracking().FirstOrDefault(i => i.Id == id);
            if (instructor != null)
            {
                cmbInstructorDepartment.SelectedValue = instructor.DepartmentId;
            }
        }

        private void btnDeleteInstructor_Click(object sender, EventArgs e)
        {
            if (dgvInstructors.SelectedRows.Count == 0) return;

            var id = (int)dgvInstructors.SelectedRows[0].Cells["Id"].Value;
            using var context = new MyDbContext();
            var instructor = context.Instructors.Find(id);
            if (instructor != null)
            {
                context.Instructors.Remove(instructor);
                context.SaveChanges();
                LoadAllData();
            }
        }

        private void btnUpdateInstructor_Click(object sender, EventArgs e)
        {
            if (dgvInstructors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an instructor to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInstructorFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtInstructorLastName.Text))
            {
                MessageBox.Show("Please enter first and last name.");
                return;
            }

            var id = (int)dgvInstructors.SelectedRows[0].Cells["Id"].Value;
            using var context = new MyDbContext();
            var instructor = context.Instructors.Find(id);
            if (instructor == null) return;

            instructor.FirstName = txtInstructorFirstName.Text;
            instructor.LastName = txtInstructorLastName.Text;
            instructor.Phone = txtInstructorPhone.Text;
            instructor.DepartmentId = (int)cmbInstructorDepartment.SelectedValue!;

            context.SaveChanges();
            LoadAllData();
            ClearInstructorFields();
        }

        private void ClearInstructorFields()
        {
            txtInstructorFirstName.Clear();
            txtInstructorLastName.Clear();
            txtInstructorPhone.Clear();
        }
        #endregion

        #region Students
        private void LoadStudents()
        {
            using var context = new MyDbContext();
            dgvStudents.DataSource = context.Students
                .Select(s => new { s.Id, s.FirstName, s.LastName, s.Phone })
                .ToList();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtStudentLastName.Text))
            {
                MessageBox.Show("Please enter first and last name.");
                return;
            }

            using var context = new MyDbContext();
            var student = new Student
            {
                FirstName = txtStudentFirstName.Text,
                LastName = txtStudentLastName.Text,
                Phone = txtStudentPhone.Text
            };
            context.Students.Add(student);
            context.SaveChanges();
            LoadAllData();
            ClearStudentFields();
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;

            var row = dgvStudents.SelectedRows[0];
            txtStudentFirstName.Text = row.Cells["FirstName"].Value?.ToString() ?? string.Empty;
            txtStudentLastName.Text = row.Cells["LastName"].Value?.ToString() ?? string.Empty;
            txtStudentPhone.Text = row.Cells["Phone"].Value?.ToString() ?? string.Empty;
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;

            var id = (int)dgvStudents.SelectedRows[0].Cells["Id"].Value;
            using var context = new MyDbContext();
            var student = context.Students.Find(id);
            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                LoadAllData();
            }
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStudentFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtStudentLastName.Text))
            {
                MessageBox.Show("Please enter first and last name.");
                return;
            }

            var id = (int)dgvStudents.SelectedRows[0].Cells["Id"].Value;
            using var context = new MyDbContext();
            var student = context.Students.Find(id);
            if (student == null) return;

            student.FirstName = txtStudentFirstName.Text;
            student.LastName = txtStudentLastName.Text;
            student.Phone = txtStudentPhone.Text;

            context.SaveChanges();
            LoadAllData();
            ClearStudentFields();
        }

        private void ClearStudentFields()
        {
            txtStudentFirstName.Clear();
            txtStudentLastName.Clear();
            txtStudentPhone.Clear();
        }
        #endregion

        #region Courses
        private void LoadCourses()
        {
            using var context = new MyDbContext();
            dgvCourses.DataSource = context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructor)
                .Select(c => new { c.Id, c.Name, c.Duration, Department = c.Department.Name, Instructor = c.Instructor.FirstName + " " + c.Instructor.LastName })
                .ToList();

            // Load departments and instructors for combo boxes
            cmbCourseDepartment.DataSource = context.Departments.ToList();
            cmbCourseDepartment.DisplayMember = "Name";
            cmbCourseDepartment.ValueMember = "DepartmentId";

            cmbCourseInstructor.DataSource = context.Instructors.ToList();
            cmbCourseInstructor.DisplayMember = "FirstName";
            cmbCourseInstructor.ValueMember = "Id";
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter course name.");
                return;
            }

            using var context = new MyDbContext();
            var course = new Course
            {
                Name = txtCourseName.Text,
                Duration = (int)numCourseDuration.Value,
                DepartmentId = (int)cmbCourseDepartment.SelectedValue!,
                InstructorId = (int)cmbCourseInstructor.SelectedValue!
            };
            context.Courses.Add(course);
            context.SaveChanges();
            LoadAllData();
            ClearCourseFields();
        }

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0) return;

            var row = dgvCourses.SelectedRows[0];
            txtCourseName.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;

            var durationText = row.Cells["Duration"].Value?.ToString();
            if (int.TryParse(durationText, out var duration))
            {
                numCourseDuration.Value = duration;
            }
            else
            {
                numCourseDuration.Value = 0;
            }

            var id = (int)row.Cells["Id"].Value;
            using var context = new MyDbContext();
            var course = context.Courses.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                cmbCourseDepartment.SelectedValue = course.DepartmentId;
                cmbCourseInstructor.SelectedValue = course.InstructorId;
            }
        }

        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0) return;

            var id = (int)dgvCourses.SelectedRows[0].Cells["Id"].Value;
            using var context = new MyDbContext();
            var course = context.Courses.Find(id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
                LoadAllData();
            }
        }

        private void btnUpdateCourse_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter course name.");
                return;
            }

            var id = (int)dgvCourses.SelectedRows[0].Cells["Id"].Value;
            using var context = new MyDbContext();
            var course = context.Courses.Find(id);
            if (course == null) return;

            course.Name = txtCourseName.Text;
            course.Duration = (int)numCourseDuration.Value;
            course.DepartmentId = (int)cmbCourseDepartment.SelectedValue!;
            course.InstructorId = (int)cmbCourseInstructor.SelectedValue!;

            context.SaveChanges();
            LoadAllData();
            ClearCourseFields();
        }

        private void ClearCourseFields()
        {
            txtCourseName.Clear();
            numCourseDuration.Value = 0;
        }
        #endregion
    }
}

