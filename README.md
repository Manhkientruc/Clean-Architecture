# Giới thiệu & Đặt vấn đề

## Tại sao cần Clean Architecture?

Khi phần mềm ngày càng phức tạp, việc quản lý codebase trở nên khó khăn nếu không có cấu trúc rõ ràng. Clean Architecture được giới thiệu như một giải pháp để xử lý các vấn đề thường gặp trong phát triển phần mềm lớn:

### 1. Tránh spaghetti code
Khi project càng lớn, logic nghiệp vụ, truy vấn database và xử lý giao diện thường bị trộn lẫn trong cùng một class. Điều này khiến code trở nên rối rắm, khó đọc và khó bảo trì.

### 2. Dễ bảo trì và mở rộng
Clean Architecture giúp phân tách rõ ràng các phần trong hệ thống. Nhờ đó:
- Khi thêm tính năng hoặc sửa lỗi, chỉ cần thay đổi đúng tầng liên quan.
- Giảm thiểu rủi ro thay đổi một chỗ làm ảnh hưởng đến chỗ khác.

### 3. Tăng khả năng kiểm thử (Testability)
- Logic nghiệp vụ (Use Cases) không phụ thuộc vào database hay UI → dễ kiểm thử độc lập.
- Viết unit test đơn giản hơn, ít cần mock, tốc độ nhanh hơn.

### 4. Giảm phụ thuộc vào công nghệ và framework
- Logic cốt lõi nằm ở tầng riêng biệt, không phụ thuộc vào ASP.NET, Entity Framework, hay bất kỳ công nghệ cụ thể nào.
- Có thể thay đổi UI, database hoặc framework mà không ảnh hưởng đến business logic.

### 5. Tăng độ bền vững cho dự án
- Khi team thay người hoặc thay đổi công nghệ, logic nghiệp vụ vẫn được bảo toàn.
- Hệ thống có thể tồn tại và phát triển lâu dài mà không bị "nát" theo thời gian.

### 6. Hỗ trợ làm việc nhóm hiệu quả hơn
- Dễ phân chia công việc theo tầng: UI, logic nghiệp vụ, data access.
- Giảm xung đột code, dễ quản lý, dễ code review.

---

## Vấn đề của Monolith Spaghetti Code

Hệ thống monolith được xây dựng không có kiến trúc rõ ràng rất dễ rơi vào tình trạng "spaghetti code", với hàng loạt vấn đề đi kèm:

### 1. Không có ranh giới rõ ràng giữa các phần
- Giao diện, logic nghiệp vụ và truy cập dữ liệu nằm lẫn lộn trong cùng một lớp.
- Khi thay đổi một phần, rất khó kiểm soát ảnh hưởng đến các phần còn lại.

### 2. Khó bảo trì và mở rộng
- Dễ phát sinh lỗi khi sửa hoặc thêm chức năng.
- Tốn thời gian hiểu code cũ và đánh giá rủi ro thay đổi.

### 3. Code phụ thuộc lẫn nhau chằng chịt
- Các thành phần gọi qua lại, gây ra phụ thuộc vòng lặp.
- Khó tách module, khó tái sử dụng.

### 4. Không thể kiểm thử độc lập
- Logic phụ thuộc trực tiếp vào tầng dữ liệu hoặc UI → khó viết test đơn vị.
- Phải kiểm thử toàn hệ thống, tốn thời gian và kém ổn định.

### 5. Khó áp dụng DevOps hoặc CI/CD
- Không thể triển khai từng phần riêng biệt.
- Mỗi thay đổi yêu cầu build và deploy lại toàn bộ hệ thống, dễ sinh lỗi.

### 6. Khó mở rộng đội ngũ phát triển
- Nhiều người cùng sửa một codebase rối rắm → xung đột thường xuyên.
- Dev mới mất nhiều thời gian để hiểu code và làm quen với hệ thống.

# Khái niệm Clean Architecture

## Định nghĩa (theo Uncle Bob)

### 1. Định nghĩa

**Clean Architecture** là một mô hình kiến trúc phần mềm được đề xuất bởi **Robert C. Martin** (Uncle Bob), với mục tiêu tổ chức hệ thống sao cho:
- Dễ hiểu  
- Dễ kiểm thử  
- Dễ bảo trì  
- Và có khả năng mở rộng linh hoạt theo thời gian

Clean Architecture nhấn mạnh việc:
- **Tách biệt logic nghiệp vụ (business rules)** ra khỏi các thành phần phụ thuộc như database, UI, framework, hay các thư viện bên ngoài.
- **Tuân thủ nguyên tắc phụ thuộc một chiều**: các tầng bên ngoài có thể phụ thuộc vào tầng bên trong, nhưng tầng bên trong **không bao giờ được phụ thuộc ngược lại**.

---

### 2. Mục tiêu chính

- Tăng tính **độc lập** của logic nghiệp vụ đối với công nghệ, framework và hạ tầng.
- Dễ dàng **kiểm thử, bảo trì và tái sử dụng** các thành phần quan trọng trong hệ thống.
- Hỗ trợ hệ thống **phát triển bền vững và linh hoạt**, có thể thay đổi công nghệ mà không ảnh hưởng đến phần cốt lõi.
- Giảm rủi ro kỹ thuật và tránh việc hệ thống bị “đóng khung” theo công nghệ ban đầu.

---

### 3. Câu nói nổi tiếng của Uncle Bob:

> **“The architecture should tell us about the system, not about the frameworks it uses.”**

Tạm dịch:

> *“Kiến trúc phần mềm nên phản ánh bản chất của hệ thống, chứ không phải framework mà nó sử dụng.”*

---

## Mục tiêu cốt lõi của Clean Architecture

### 1. Tách biệt rõ ràng giữa các tầng trong hệ thống
- Mỗi tầng có một vai trò riêng biệt: từ xử lý logic nghiệp vụ đến giao tiếp với bên ngoài.
- Giúp hệ thống có cấu trúc rõ ràng, dễ đọc, dễ quản lý và dễ nâng cấp.

### 2. Bảo vệ logic nghiệp vụ khỏi công nghệ bên ngoài
- Logic nghiệp vụ không phụ thuộc vào framework, cơ sở dữ liệu hoặc UI.
- Việc thay đổi công nghệ (VD: từ SQL sang NoSQL) không ảnh hưởng đến tầng cốt lõi.

### 3. Dễ dàng kiểm thử (Testability)
- Do mỗi tầng hoạt động độc lập nên có thể kiểm thử từng phần riêng lẻ.
- Việc viết unit test cho logic nghiệp vụ trở nên đơn giản, nhanh và đáng tin cậy.

### 4. Hỗ trợ bảo trì và phát triển lâu dài
- Giảm "nợ kỹ thuật" nhờ cấu trúc rõ ràng, có định hướng.
- Logic nghiệp vụ có thể tiến hóa mà không làm ảnh hưởng đến tầng ngoài.

### 5. Tăng tính tái sử dụng và mở rộng
- Các module có thể dễ dàng tái sử dụng ở nhiều bối cảnh khác nhau.
- Hệ thống dễ dàng mở rộng theo chiều ngang bằng cách tách các phần độc lập.


# Các tầng (Layers) chính

## Entities (Domain Models)

### Định nghĩa

`Entities` là tầng sâu nhất và quan trọng nhất trong Clean Architecture.  
Nó chứa các đối tượng cốt lõi mô tả nghiệp vụ (**business objects**) và các quy tắc nghiệp vụ cấp cao (**enterprise rules**).

### Đặc điểm chính

- Độc lập hoàn toàn với bất kỳ công nghệ nào như framework, database, hay UI.
- Không phụ thuộc vào bất kỳ tầng nào khác trong hệ thống.
- Là nơi lưu trữ logic nghiệp vụ bền vững, có thể tồn tại trong nhiều năm dù công nghệ thay đổi.

### Ví dụ

Giả sử đang xây dựng hệ thống quản lý đơn hàng, thì các entity có thể là: `Order`, `Customer`, `Product`.

Mỗi entity thường bao gồm:
- **Thuộc tính:** `OrderId`, `CustomerId`, `TotalAmount`,...
- **Quy tắc nghiệp vụ:** `Order.CalculateTotal()`, `Order.CanBeCancelled()`...

```csharp
public class Order
{
    public Guid Id { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

    public bool CanBeCancelled(DateTime now)
    {
        return (now - CreatedAt).TotalHours < 24;
    }

    public DateTime CreatedAt { get; private set; }
}
```

### Vai trò trong hệ thống

- Là nền tảng ổn định nhất, nơi các tầng khác (Use Cases, Adapters, Frameworks) đều phụ thuộc vào.
- Đảm bảo hệ thống phản ánh đúng các quy tắc nghiệp vụ, không chỉ đơn thuần là thao tác dữ liệu.

## Use Cases / Application Layer
### Định nghĩa

Use Cases (hay còn gọi là Application Layer) là tầng chịu trách nhiệm triển khai các **quy trình nghiệp vụ cụ thể** của hệ thống.  
Tầng này sử dụng các `Entities` để thực hiện nghiệp vụ, nhưng **không biết gì về UI, database hay framework bên ngoài**.

---

### Chức năng chính

- Thực thi các hành động nghiệp vụ như: “Tạo đơn hàng”, “Xử lý thanh toán”, “Gửi email xác nhận”,...
- Đóng vai trò như một "điều phối viên" giữa các tầng: gọi repository, kiểm tra logic nghiệp vụ, xử lý kết quả và trả về tầng ngoài (API/Controller).

---

### Đặc điểm

- Phụ thuộc vào `Entities`, nhưng **không phụ thuộc vào UI hoặc tầng dữ liệu**.
- Chứa logic nghiệp vụ ở cấp ứng dụng, không phải logic nghiệp vụ cốt lõi.
- Các hành động thường được viết thành **service**, **command handler** hoặc **interactor**.

---

### Ví dụ

```csharp
public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.Price);
        }

        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}

```

### Vai trò trong hệ thống
- Là cầu nối giữa logic nghiệp vụ (Entities) và thế giới bên ngoài (UI, DB, API).
- Giúp hệ thống có thể thay đổi UI hay database mà không ảnh hưởng đến logic xử lý nghiệp vụ.

## Interface Adapters (Controllers, Gateways, Presenters)
### Định nghĩa
Interface Adapters là tầng chuyển đổi dữ liệu và điều phối giao tiếp giữa hệ thống bên trong và bên ngoài.
Nó đóng vai trò “trung gian dịch thuật” giữa các Use Case và công nghệ như Web API, cơ sở dữ liệu, hoặc UI.
### Chức năng chính
    • Nhận dữ liệu từ UI/API, chuyển thành dạng mà Use Case có thể xử lý (Input DTO).
    • Gọi các Use Case để xử lý logic nghiệp vụ.
    • Chuyển kết quả từ Use Case thành dữ liệu phù hợp để trả ra ngoài (ViewModel, JSON…).
    • Giao tiếp với database qua interface repository (thường là implementation của IGateway hoặc IRepository).
### Thành phần thường có
    • Controllers: nhận request từ phía người dùng (API, Web).
    • Gateways (Repositories): triển khai giao tiếp với cơ sở dữ liệu hoặc service bên ngoài.
    • Presenters / View Models: chuẩn bị dữ liệu để trả về UI hoặc API response.
### Ví dụ
1. Controllers
```csharp
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _createOrderUseCase;

    public OrdersController(CreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var orderId = await _createOrderUseCase.ExecuteAsync(request);
        return Ok(new { Id = orderId });
    }
}
```

2. Repository (Gateway Implementation)
```csharp
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task AddAsync(Order order)
    {
        var entity = OrderMapper.ToEntity(order);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }
}
```

### Vai trò trong hệ thống
Đảm bảo tầng trong (Application, Entities) không bị phụ thuộc vào các công nghệ bên ngoài như ASP.NET, EF Core, hay các API cụ thể.
Dễ dàng thay đổi giao diện (UI, Web API) hay thay đổi hạ tầng (database) mà không ảnh hưởng đến logic cốt lõi.

## Frameworks & Drivers (UI, DB, External APIs)
### Định nghĩa
Frameworks & Drivers là tầng ngoài cùng trong Clean Architecture, nơi chứa các thành phần cụ thể phụ thuộc vào công nghệ, chẳng hạn như:
Giao diện người dùng (Web UI, Blazor, Razor Pages…)
Framework backend (ASP.NET Core)
Database (SQL Server, MongoDB, PostgreSQL…)
Các dịch vụ bên ngoài (REST API, Message Queue, Email, File Storage, v.v.)

### Đặc điểm chính
Đây là nơi "đi vào và đi ra" của hệ thống, chịu trách nhiệm giao tiếp với thế giới bên ngoài.
Phụ thuộc vào các tầng bên trong, không được để tầng bên trong phụ thuộc lại.
Thường xuyên thay đổi theo yêu cầu công nghệ, nhưng logic cốt lõi không bị ảnh hưởng.

### Chức năng chính
Giao tiếp và triển khai cụ thể các cổng (gateways) được định nghĩa bởi tầng Application (Use Case).
Ví dụ:
ASP.NET Controller nằm ở đây, nhưng phụ thuộc vào interface ở Application.
Entity Framework DbContext nằm ở đây, triển khai repository interface của tầng trên.
Giao diện frontend (React, Angular) có thể gọi vào tầng Web API ở tầng này.
### Ví dụ
```csharp
public class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task<Order> GetByIdAsync(Guid id)
    {
        var entity = await _context.Orders.FindAsync(id);
        return OrderMapper.ToDomain(entity);
    }
}
```

### Vai trò trong hệ thống
Là nơi cắm công nghệ vào hệ thống, nhưng theo cách có kiểm soát.
Cho phép thay thế dễ dàng: từ EF → Dapper, hoặc từ REST → gRPC mà không động đến business logic.

# Nguyên tắc phụ thuộc (Dependency Rule)
## Luồng phụ thuộc từ “ngoài vào trong”
### Định nghĩa
Dependency Rule là nguyên tắc cốt lõi trong Clean Architecture, quy định rằng các tầng bên ngoài có thể phụ thuộc vào tầng bên trong, nhưng tầng bên trong tuyệt đối không được phụ thuộc ngược lại.

### Luồng phụ thuộc
Luồng phụ thuộc trong Clean Architecture luôn đi theo hướng:
Frameworks & Drivers → Interface Adapters → Use Cases → Entities
Nghĩa là:
    Use Cases có thể gọi Entities.
    Interface Adapters có thể gọi Use Cases.
    Frameworks có thể gọi Controller, Repository, Service ở tầng Adapter.
    Nhưng không bao giờ ngược lại.

### Ý nghĩa
Entities và Use Cases không được biết gì về web, database, hay giao diện người dùng.
Giúp hệ thống tách biệt hoàn toàn logic nghiệp vụ với công nghệ cụ thể.
Khi cần thay đổi công nghệ (ví dụ: từ ASP.NET MVC sang Blazor), bạn chỉ cần sửa các lớp ở tầng ngoài mà không ảnh hưởng đến logic bên trong.

### Ví dụ minh hoạ
Giả sử có class OrderController ở tầng Web (Frameworks), gọi vào CreateOrderUseCase ở tầng Application:

```csharp
var orderId = await _createOrderUseCase.ExecuteAsync(request);
```

Ngược lại, bạn không được viết logic trong Use Case kiểu như:

```csharp
var result = _dbContext.Orders.ToList();
```

### Giải pháp để giữ đúng nguyên tắc
Dùng interface (abstraction) để định nghĩa hành vi cần thiết (ví dụ: IOrderRepository).
Các tầng trong chỉ gọi qua interface.
Tầng ngoài chịu trách nhiệm triển khai interface (ví dụ: OrderRepository : IOrderRepository).

## Tầng trong không biết gì về tầng ngoài
Tới luôn phần này – **“Tầng trong không biết gì về tầng ngoài”** – là một trong những điểm mấu chốt làm nên sự “sạch sẽ” của Clean Architecture.
Dưới đây là nội dung rõ ràng – chặt chẽ – dễ hiểu để bạn đưa vào Word hoặc README:

---

## Tầng trong không biết gì về tầng ngoài

### Nguyên tắc cốt lõi

Trong Clean Architecture, tầng trong (ví dụ: `Entities`, `Use Cases`) không được phép biết bất kỳ điều gì về tầng ngoài như:

 Giao diện người dùng (UI)
 Cơ sở dữ liệu (DB)
 Framework (ASP.NET, Entity Framework, Blazor,…)
 API hoặc dịch vụ bên ngoài (Email, Message Queue,…)

Tầng trong chỉ biết đến những abstraction (giao diện), chứ không quan tâm cách mà tầng ngoài thực thi cụ thể.

### Vì sao lại như vậy?

 Để bảo vệ logic nghiệp vụ – phần cốt lõi của hệ thống – khỏi những thay đổi liên quan đến công nghệ.
 Đảm bảo code ở tầng trong luôn dễ test, dễ đọc và dễ tái sử dụng.
 Tránh tình trạng code business logic bị "dính chặt" với framework hoặc database cụ thể.

### Cách thực hiện

Để tầng trong không phụ thuộc tầng ngoài, Clean Architecture thường dùng:

Dependency Inversion Principle (DIP) – Đảo ngược sự phụ thuộc.
Interface-based Programming – Các tầng trong định nghĩa interface (ví dụ: `IOrderRepository`), tầng ngoài implement nó.

> Tầng trong “ra yêu cầu”, tầng ngoài “đáp ứng”.


### Ví dụ minh hoạ

2. Tầng trong:

```csharp
public interface IOrderRepository
{
    Task AddAsync(Order order);
}
```

1. Tầng ngoài:

```csharp
public class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task AddAsync(Order order)
    {
        var entity = OrderMapper.ToEntity(order);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }
}
```

Ở đây:
`Use Case` chỉ gọi `IOrderRepository`, không biết gì về AppDbContext hay EF Core.
EF Core được sử dụng chỉ ở tầng ngoài (Frameworks & Drivers).

# Ưu & nhược điểm
## Ưu điểm: maintainable, testable, scalable…
## Nhược điểm: learning curve, boilerplate ban đầu
# So sánh với các kiến trúc khác
## MVC / Layered Architecture
## Onion Architecture
## Hexagonal (Ports & Adapters)
# Áp dụng trong .NET
## Cấu trúc thư mục & project solution
## Ví dụ interface, DTO, service, repository
## Thư viện & template nổi bật (Jason Taylor, Ardalis)
# Best Practices & Tips
## Organization: tên project, naming convention
## Cách test từng layer
## Dependency Injection trong ASP.NET Core
# Các ví dụ minh hoạ
## Code snippet minh họa flow (từ Controller → Use Case → Entity)
## Sơ đồ sequence hoặc class diagram
# Kết luận & Hướng triển khai
## Các bước “bật đèn xanh” cho dự án .NET mới
## Lộ trình tiếp theo: refactor, mở rộng, tích hợp AI
