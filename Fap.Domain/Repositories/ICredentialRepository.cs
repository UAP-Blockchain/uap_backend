using Fap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fap.Domain.Repositories
{
    public interface ICredentialRepository
    {
        Task<Credential?> GetByIdAsync(Guid id);
        Task<IEnumerable<Credential>> GetAllAsync();
        Task<IEnumerable<Credential>> FindAsync(Expression<Func<Credential, bool>> predicate);
        Task<Credential> AddAsync(Credential entity);
        void Update(Credential entity);
        void Delete(Credential entity);
        Task<int> CountAsync(Expression<Func<Credential, bool>>? predicate = null);
    }

    public interface ICredentialRequestRepository
    {
        Task<CredentialRequest?> GetByIdAsync(Guid id);
        Task<IEnumerable<CredentialRequest>> GetAllAsync();
        Task<IEnumerable<CredentialRequest>> FindAsync(Expression<Func<CredentialRequest, bool>> predicate);
        Task<CredentialRequest> AddAsync(CredentialRequest entity);
        void Update(CredentialRequest entity);
        void Delete(CredentialRequest entity);
        Task<int> CountAsync(Expression<Func<CredentialRequest, bool>>? predicate = null);
    }

    public interface ICertificateTemplateRepository
    {
        Task<CertificateTemplate?> GetByIdAsync(Guid id);
        Task<IEnumerable<CertificateTemplate>> GetAllAsync();
        Task<IEnumerable<CertificateTemplate>> FindAsync(Expression<Func<CertificateTemplate, bool>> predicate);
        Task<CertificateTemplate> AddAsync(CertificateTemplate entity);
        void Update(CertificateTemplate entity);
        void Delete(CertificateTemplate entity);
    }

    public interface ISubjectCriteriaRepository
    {
        Task<SubjectCriteria?> GetByIdAsync(Guid id);
        Task<IEnumerable<SubjectCriteria>> GetAllAsync();
        Task<IEnumerable<SubjectCriteria>> FindAsync(Expression<Func<SubjectCriteria, bool>> predicate);
        Task<SubjectCriteria> AddAsync(SubjectCriteria entity);
        void Update(SubjectCriteria entity);
        void Delete(SubjectCriteria entity);
    }
}
