# Đồ Án Nhập Môn Công Nghệ Phần Mềm
# Project Quản Lý Hội Nghị
# Lớp 18_1
# Nhóm 01

# --------------------------- #
# ----- Thành viên nhóm ----- #
# --------------------------- #
# 18120655 - Phạm Minh Vương  #
# 18120562 - Đặng Minh Thành  #
# 18120576 - Nguyễn Hữu Thịnh #
# 18120606 - Trần Thị Trang   #
# --------------------------- #
# --------------------------- #

# I. Cơ sở dữ liệu:
    - Hội nghị bao gồm các thông tin: tên, mô tả ngắn gọn, mô tả chi tiết, hình ảnh, thời gian tổ chức, địa điểm tổ chức, người tham dự (có số lượng giới hạn và phụ thuộc vào nơi tổ chức).
    - Địa điểm tổ chức có thông tin: tên, địa chỉ, sức chứa.
    - Admin có các thông tin: tên, username, password (được mã hóa), email.
    - User có các thông tin như admin.
    - Các vấn đề ràng buộc (địa điểm không thể sử dụng chung trong cùng thời điểm), phân quyền,…

# II. Các chức năng cần có
    1. Đối với phân hệ Khách:
        - Màn hình chính giới thiệu chương trình và danh sách các Hội nghị với thông tin ngắn gọn.
            + Cho phép sắp xếp: Tăng dần, giảm dần.
            + Cho phép tìm kiếm theo tên, địa điểm và thời gian tổ chức hội nghị.
            + Sử dụng Full Text Search trong tìm kiếm tên hội nghị và tên địa điểm.
            
        - Màn hình xem chi tiết Hội nghị với đầy đủ thông tin của Hội nghị và cho phép đăng ký tham dự (đề xuất lựa chọn nếu chưa có account thì đăng ký account, nếu đã có account thì đăng nhập và đăng ký tham dự).
        
    2. Đối với phân hệ người dùng (user) đã đăng nhập:
        - Có đủ các chức năng của phân hệ Khách.
        
        - Màn hình profile để xem và chỉnh sửa thông tin cá nhân.

        - Màn hình thống kê các Hội nghị đã đăng ký tham dự.
            + Cho phép sắp xếp: Tăng dần, giảm dần.
            + Cho phép tìm kiếm theo tên, địa điểm và thời gian tổ chức hội nghị.
            + Sử dụng Full Text Search trong tìm kiếm tên hội nghị và tên địa điểm.

    3. Đối với phân hệ Admin:
        - Màn hình quản lý Hội nghị: 
            + Thêm mới.
            + Sửa đổi Hội nghị chưa diễn ra. 
            + Chấp nhận yêu cầu tham dự của User.
            
        - Màn hình quản lý User: 
            + Xem danh sách.
            + Ngăn chặn truy cập.
            + Sắp xếp và lọc theo nhiều tiêu chí.
        
        
        
        
        
        
        
        
        
        
        